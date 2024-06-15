using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class StopTransition : ITransition
    {
        private StopState stopState;
        private CarAI carAI;
        private Vector3 nearest;

        public StopTransition(BaseState stopState, CarAI carAI)
        {
            this.stopState = stopState as StopState;
            this.carAI = carAI;
            nearest = FindNearestWayToGraph(carAI.parkPosition);
            nearest = carAI.parkPosition + (nearest - carAI.parkPosition).normalized * 2.5f;
        }

        public BaseState GetNextState()
        {
            return stopState;
        }

        public bool IsValid()
        {
            //Debug.Log(Vector2.SqrMagnitude(carAI.transform.position - nearest - carAI.transform.right * 1f));
            return Vector2.SqrMagnitude(carAI.transform.position - nearest - carAI.transform.right * 1f) <= 0.1f;
        }

        public void OnTransition()
        {
            carAI.ChangeDestination();
        }

        private Vector3 FindNearestWayToGraph(Vector3 position)
        {
            //Segment edge;
            float currentDistance = Mathf.Infinity;
            Vector3 pos = Vector3.zero;
            var edges = RoadWaypoints.Instance.Edges;
            //edge = edges[0];
            for (int i = 0; i < edges.Length; i++)
            {
                var newpos = RoadWaypointsHelper.FindNearestPointOnLine(edges[i], position);
                float sqrDistance = Vector2.SqrMagnitude(newpos - position);
                if (sqrDistance < currentDistance)
                {
                    pos = newpos;
                    currentDistance = sqrDistance;
                    //edge = edges[i];
                }
            }
            //pos = RoadWaypointsHelper.FindNearestPointOnLine(edges[0], transform.position);
            return pos;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class ParkTransition : ITransition
    {
        private ParkState parkState;
        private CarAI carAI;
        private Vector3 nearest;

        public ParkTransition(ParkState parkState, CarAI carAI)
        {
            this.parkState = parkState as ParkState;
            this.carAI = carAI;
            nearest = FindNearestWayToGraph(carAI.parkPosition);
        }

        public BaseState GetNextState()
        {
            return parkState;
        }

        public bool IsValid()
        {
            return Vector2.SqrMagnitude(carAI.transform.position - carAI.transform.right * 3.5f - nearest) <= 0.1f;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;


namespace Carpark.AI.FSM
{
    public class StopState : BaseState
    {
        private CarAI carAI;
        private Vector3 nearest;
        private Vector3 stopPosition;

        public StopState(CarController controller) : base(controller)
        {
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            nearest = FindNearestWayToGraph(carAI.parkPosition);
            stopPosition = nearest + carAI.transform.right * 5f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (m_controller.Velocity < 1f && Vector2.Distance(stopPosition, m_controller.transform.position) > 1)
                m_controller.Throtte();
            else
                m_controller.Brake();
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

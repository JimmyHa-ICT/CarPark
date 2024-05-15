using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class ParkState : BaseState
    {
        private CarAI carAI;
        private Vector3 nearest;

        public ParkState(CarController controller) : base(controller)
        {
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_controller.fwMode = -m_controller.fwMode;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Vector2 direction = (nearest - carAI.parkPosition).normalized;
            float angle = Vector2.SignedAngle(m_controller.transform.right, direction);

            m_controller.Steer(angle);

            if (m_controller.Velocity < 0.25f && Vector2.Distance(m_controller.transform.position, carAI.parkPosition) > 0.1f)
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

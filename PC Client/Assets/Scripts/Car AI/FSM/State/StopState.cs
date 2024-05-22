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
        private Vector3 orientation;

        public StopState(CarController controller) : base(controller)
        {
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            nearest = FindNearestWayToGraph(carAI.parkPosition);
            orientation = Vector3.Normalize(carAI.parkPosition - nearest);
            stopPosition = nearest + carAI.transform.right * 5f ;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            float angle = Vector2.SignedAngle(m_controller.transform.right, -orientation);
            m_controller.Steer(angle);
            if (carAI.CheckObscuring().collider != null)
            {
                Debug.Log("Brake");
                m_controller.Brake();
            }
            else
            {
                if (m_controller.Velocity < 1f)
                    m_controller.Throtte();
                else
                    m_controller.Brake();
            }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;
using DG.Tweening;

namespace Carpark.AI.FSM
{
    public class ParkState : BaseState
    {
        private CarAI carAI;
        private Vector3 nearest;
        private Vector2 direction;
        private bool rightRotation;

        public ParkState(CarController controller) : base(controller)
        {
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_controller.fwMode = -m_controller.fwMode;
            nearest = FindNearestWayToGraph(carAI.parkPosition);
            direction = (nearest - carAI.parkPosition).normalized;
            Debug.Log(direction);
            rightRotation = false;
            m_controller.steer = 180;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Debug.DrawRay(m_controller.transform.position, direction, Color.blue);
            Debug.DrawRay(m_controller.transform.position, m_controller.transform.right, Color.red);
            float angle = Vector2.SignedAngle(m_controller.transform.right, direction);
            Debug.Log(angle);
            //m_controller.transform.rotation = Quaternion.Slerp(m_controller.transform.rotation, Quaternion.Euler(0, 0, 90), 10 * Time.deltaTime);
            //m_controller.transform.right = Vector3.Slerp(m_controller.transform.right, direction, 10 * Time.deltaTime);
            if (Mathf.Abs(angle) < 0.1f)
            {
                Debug.LogError("right rotation");
                rightRotation = true;
            }    
            
            if (rightRotation)
            {
                m_controller.Steer(0);
                Debug.Log("Steer zero");
            }    
            else
                m_controller.Steer(angle);


            if (!rightRotation && m_controller.Velocity < 1f)
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

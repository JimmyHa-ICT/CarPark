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
        private Vector2 orientation;
        private Vector2 direction;
        private bool rightRotation;

        private float timeout;

        public ParkState(CarController controller) : base(controller)
        {
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            m_controller.fwMode = -1;
            nearest = RoadWaypointsHelper.FindNearestWayToGraph(carAI.parkPosition);
            orientation = (nearest - carAI.parkPosition).normalized;
            Debug.Log(orientation);
            rightRotation = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Debug.DrawRay(m_controller.transform.position, direction, Color.blue);
            Debug.DrawRay(m_controller.transform.position, m_controller.transform.right, Color.red);
            if (m_controller.fwMode == -1)
            {
                direction = m_controller.transform.position - carAI.parkPosition;
                float angle = Vector2.SignedAngle(m_controller.transform.right, direction);
                float angleOrientation = Vector2.SignedAngle(m_controller.transform.right, orientation);
                //Debug.Log(angleOrientation);

                if (Mathf.Abs(angleOrientation) < 0.4f)
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
                    m_controller.Steer(Mathf.Clamp(angleOrientation, -3, 3));

                float distanceAxis = Vector2.Dot(m_controller.transform.position - carAI.parkPosition,
                                                        (nearest - carAI.parkPosition).normalized);
                if (!rightRotation && m_controller.Velocity < 1f && distanceAxis > 0.05f)
                    m_controller.Throtte();
                else
                    m_controller.Brake();

                var hit = carAI.CheckObscuring(0.7f);
                if (hit || (Vector2.Dot(m_controller.transform.position - carAI.parkPosition, 
                                                        (nearest - carAI.parkPosition).normalized) < -0.05f && rightRotation))
                {
                    Debug.Log("Hit back " + hit.collider);
                    m_controller.fwMode = 1;
                    timeout = 2;
                }
            }
            else
            {
                timeout -= Time.deltaTime;
                float distanceAxis = Vector2.Dot(m_controller.transform.position - carAI.parkPosition,
                                                        (nearest - carAI.parkPosition).normalized);
                if (timeout < 0 || carAI.CheckObscuring(0.8f) || (distanceAxis > 0.05f && rightRotation))
                {
                    m_controller.fwMode = -1;
                }

                float angleOrientation = Vector2.SignedAngle(m_controller.transform.right, orientation);
                if (rightRotation)
                {
                    m_controller.Steer(0);
                    Debug.Log("Steer zero");
                }
                else
                    m_controller.Steer(Mathf.Clamp(angleOrientation, -3, 3));

                if (m_controller.Velocity < 0.6f && Mathf.Abs(distanceAxis) > 0.05f)
                    m_controller.Throtte();
                else
                    m_controller.Brake();
            }    
        }

        public override void OnExit()
        {
            base.OnExit();
            m_controller.fwMode = 1;

        }
    }
}

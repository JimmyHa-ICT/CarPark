using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class PerpendicularOutState : BaseState
    {
        private CarAI carAI;
        private Vector3 startPos;
        private Vector3 destination;
        private Vector3 nextDestination;
        private Vector3 orientation;
        private Vector3 nearest;

        private int step;

        public PerpendicularOutState(CarController controller) : base(controller)
        {
            m_controller = controller;
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("OnEnter");
            m_controller.fwMode = 1;
            orientation = carAI.Path[1] - carAI.Path[0];
            Debug.Log(orientation);
            startPos = m_controller.transform.position;
            nearest = RoadWaypointsHelper.FindNearestWayToGraph(carAI.parkPosition);
            step = 0;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            switch (step)
            {
                case 0:
                    m_controller.fwMode = 1;
                    float angleOrientation = Vector2.SignedAngle(m_controller.transform.right, orientation);
                    m_controller.Steer(Mathf.Clamp(angleOrientation, -3, 3));
                    if (m_controller.Velocity < 1f)
                        m_controller.Throtte();
                    else
                        m_controller.Brake();

                    Vector3 displace = m_controller.transform.position - startPos;
                    if (Vector3.Dot(displace, (nearest - startPos).normalized) >= 0.7f)
                    {
                        m_controller.Brake();
                        step = 1;
                    }    
                    break;
                case 1:
                    m_controller.fwMode = -1;
                    angleOrientation = Vector2.SignedAngle(m_controller.transform.right, orientation);
                    m_controller.Steer(Mathf.Clamp(angleOrientation, -3, 3));
                    if (m_controller.Velocity < 1f)
                        m_controller.Throtte();
                    else
                        m_controller.Brake();
                    
                    displace = m_controller.transform.position - startPos;
                    if (Vector3.Dot(displace, (nearest - startPos).normalized) <= 0.66f)
                    {
                        m_controller.Brake();
                        step = 2;
                    }    
                    break;
                case 2:
                    m_controller.fwMode = 1;
                    angleOrientation = Vector2.SignedAngle(m_controller.transform.right, orientation);
                    m_controller.Steer(Mathf.Clamp(angleOrientation, -3, 3));
                    var hit = carAI.CheckObscuring(0.7f);

                    if (m_controller.Velocity < 1f && !hit)
                        m_controller.Throtte();
                    else
                        m_controller.Brake();
                    break;
            }    

        }
    }
}

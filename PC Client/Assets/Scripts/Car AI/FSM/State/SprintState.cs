using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;

namespace Carpark.AI.FSM
{
    public class SprintState : BaseState
    {
        private CarController m_controller;
        private CarAI carAI;
        private Vector3 destination;

        public SprintState(CarController carController) : base(carController)
        {
            m_controller = carController;
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //Debug.Log(m_controller.transform.eulerAngles);
            destination = carAI.destination;
        }

        public override void OnUpdate()
        {
            Vector2 direction = destination - m_controller.transform.position;
            if (direction.magnitude > 1)
            {
                float angle = Vector2.SignedAngle(m_controller.transform.right, direction);
                m_controller.Steer(Mathf.Clamp(angle, -2, 2));
            }

            base.OnUpdate();
            if (carAI.CheckObscuring(1.5f).collider != null)
            {
                //Debug.Log("Brake");
                m_controller.Brake();
            }
            else if (m_controller.Velocity < 4f)
                m_controller.Throtte();
        }
    }
}

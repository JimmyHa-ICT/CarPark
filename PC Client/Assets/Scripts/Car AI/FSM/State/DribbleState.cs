using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;

namespace Carpark.AI.FSM
{
    public class DribbleState : BaseState
    {
        private Vector3 destination;
        private CarAI carAI;

        public DribbleState(CarController carController) : base(carController)
        {
            m_controller = carController;
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            destination = carAI.destination;
        }

        public override void OnUpdate()
        {
            Vector2 direction = destination - m_controller.transform.position;
            float angle = Vector2.SignedAngle(m_controller.transform.right, direction);

            m_controller.Steer(Mathf.Clamp(angle, -3, 3));

            if (m_controller.Velocity < Mathf.Lerp(1.5f, 0.5f, Mathf.Abs(angle) / 90f))
                m_controller.Throtte();
            else
                m_controller.Brake();
        }

    }
}


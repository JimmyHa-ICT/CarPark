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

        public SprintState(CarController carController) : base(carController)
        {
            m_controller = carController;
            carAI = m_controller.GetComponent<CarAI>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log(m_controller.transform.eulerAngles);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (carAI.CheckObscuring().collider != null)
            {
                Debug.Log("Brake");
                m_controller.Brake();
            }
            else if (m_controller.Velocity < 4f)
                m_controller.Throtte();
        }
    }
}

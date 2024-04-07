using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carpark.AI.FSM
{
    public class SprintState : BaseState
    {
        private CarController m_controller;

        public SprintState(CarController carController) : base(carController)
        {
            m_controller = carController;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            m_controller.Throtte();
        }
    }
}

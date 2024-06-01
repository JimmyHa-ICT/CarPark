using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carpark.AI.FSM
{
    public class IdleState : BaseState
    {
        public IdleState(CarController carControler) : base(carControler)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Enter idle state");
        }

        public override void OnUpdate()
        {
            if (m_controller.Velocity > 0)
                m_controller.Brake();
        }
    }
}

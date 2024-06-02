using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carpark.AI.FSM
{
    public class ParentState : BaseState
    {
        public FSM FSM;

        public ParentState(CarController controller) : base(controller)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            FSM.Init();
            FSM.CurrentState.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            FSM.Update();
        }

        public override void OnExit()
        {
            base.OnExit();
            FSM.CurrentState.OnExit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;


namespace Carpark.AI.FSM
{
    public class PerpendicularOutExitTransition : ITransition
    {
        private BaseState nextState;
        private CarAI carAI;
        private Vector3 orientation;

        public PerpendicularOutExitTransition(BaseState nextState, CarAI carAI)
        {
            this.nextState = nextState;
            this.carAI = carAI;
        }

        public BaseState GetNextState()
        {
            return nextState;
        }

        public bool IsValid()
        {
            orientation = carAI.Path[1] - carAI.Path[0];
            if (Vector2.Angle(carAI.transform.right, orientation) <= 0.5f)
                return true;
            else
                return false;
        }

        public void OnTransition()
        {
            carAI.ChangeDestination();
        }
    }
}

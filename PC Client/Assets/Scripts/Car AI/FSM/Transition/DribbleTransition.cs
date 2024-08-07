using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;

namespace Carpark.AI.FSM
{
    public class DribbleTransition : ITransition
    {
        private DribbleState dribbleState;
        private CarAI carAI;
        
        public DribbleTransition(BaseState dribbleState, CarAI carAI)
        {
            this.dribbleState = dribbleState as DribbleState;
            this.carAI = carAI;
        }

        public BaseState GetNextState()
        {
            return dribbleState;
        }

        public bool IsValid()
        {
            return Vector2.SqrMagnitude(carAI.transform.position - carAI.destination) <= 15f && !carAI.ReachFinalDestination();
        }

        public void OnTransition()
        {
            carAI.ChangeDestination();
        }
    }
}

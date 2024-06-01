using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class ParkToStopTransition : ITransition
    {
        private StopState stopState;
        private CarAI carAI;

        public ParkToStopTransition(BaseState stopState, CarAI carAI)
        {
            this.stopState = stopState as StopState;
            this.carAI = carAI;
        }

        public BaseState GetNextState()
        {
            return stopState;
        }

        public bool IsValid()
        {
            return carAI.CheckObscuring(0.6f);
        }

        public void OnTransition()
        {
            //throw new System.NotImplementedException();
        }
    }
}
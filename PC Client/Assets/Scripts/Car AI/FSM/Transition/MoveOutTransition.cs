using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class MoveOutTransition : ITransition
    {
        private ParentState outState;
        private CarAI carAI;
        private float timeOut;

        public MoveOutTransition(BaseState inState, CarAI carAI)
        {
            this.outState = inState as ParentState;
            this.carAI = carAI;
            timeOut = Random.Range(10, 20);
        }

        public BaseState GetNextState()
        {
            return outState;
        }

        public bool IsValid()
        {
            timeOut -= Time.deltaTime;
            return timeOut <= 0;
        }

        public void OnTransition()
        {
            timeOut = Random.Range(10, 20);
            carAI.GetPathOutParkingLot();
            carAI.destination = carAI.Path[0];
        }
    }
}
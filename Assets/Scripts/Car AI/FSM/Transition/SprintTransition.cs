using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;

namespace Carpark.AI.FSM
{
    public class SprintTransition : ITransition
    {
        private SprintState sprintState;
        private CarAI carAI;

        public SprintTransition(BaseState sprintState, CarAI carAI)
        {
            this.sprintState = sprintState as SprintState;
            this.carAI = carAI;
        }

        public BaseState GetNextState()
        {
            return sprintState;
        }

        public bool IsValid()
        {
            Vector2 path = carAI.destination - carAI.transform.position;
            return Vector2.Angle(path, carAI.transform.right) < 1f;
        }

        public void OnTransition()
        {
        }
    }
}
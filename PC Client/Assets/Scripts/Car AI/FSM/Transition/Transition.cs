using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carpark.AI.FSM
{
    public interface ITransition
    {
        public bool IsValid();

        public BaseState GetNextState();

        public void OnTransition();
    }
}

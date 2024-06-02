using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Carpark.AI.FSM
{
    public class FSM
    {
        public List<BaseState> States;
        public BaseState CurrentState;
        
        public FSM(List<BaseState> States)
        {
            this.States = States;
            CurrentState = States[0];
        }


        public void Init()
        {
            CurrentState = States[0];
        }

        public void Update()
        {
            for (int i = 0; i < CurrentState.Transitions.Count; i++)
            {
                if (CurrentState.Transitions[i].IsValid())
                {
                    CurrentState.OnExit();
                    CurrentState.Transitions[i].OnTransition();
                    CurrentState = CurrentState.Transitions[i].GetNextState();
                    CurrentState.OnEnter();
                    return;
                }
            }
            CurrentState.OnUpdate();
        }
    }
}

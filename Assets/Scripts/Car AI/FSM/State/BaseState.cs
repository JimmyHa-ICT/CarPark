using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carpark.AI.FSM
{
    public class BaseState : MonoBehaviour
    {
        protected CarController m_controller;
        public List<ITransition> Transitions;

        public BaseState(CarController controller)
        {
            Transitions = new List<ITransition>();
            m_controller = controller;

        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}

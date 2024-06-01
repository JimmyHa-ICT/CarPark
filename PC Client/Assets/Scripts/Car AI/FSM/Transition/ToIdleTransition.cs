using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class ToIdleTransition : ITransition
    {
        private IdleState idleState;
        private CarAI carAI;
        private Vector3 nearest;
        private CarController m_controller;

        public ToIdleTransition(BaseState idleState, CarAI carAI)
        {
            this.idleState = idleState as IdleState;
            this.carAI = carAI;
            m_controller = carAI.GetComponent<CarController>();
        }
        public BaseState GetNextState()
        {
            return idleState;
        }

        public bool IsValid()
        {
            nearest = RoadWaypointsHelper.FindNearestWayToGraph(carAI.parkPosition);
            var orientation = nearest - carAI.parkPosition;
            float distanceAxis = Vector2.Dot(m_controller.transform.position - carAI.parkPosition,
                                                        (nearest - carAI.parkPosition).normalized);
            float angleOrientation = Vector2.SignedAngle(m_controller.transform.right, orientation);
            //Debug.Log(angleOrientation);

            if (Mathf.Abs(angleOrientation) < 0.4f && Mathf.Abs(distanceAxis) < 0.04f)
                return true;
            else
                return false;
        }

        public void OnTransition()
        {
        }
    }
}
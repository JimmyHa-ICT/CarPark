using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class ParkTransition : ITransition
    {
        private ParkState parkState;
        private CarAI carAI;
        private Vector3 nearest;
        private Vector2 orientation;

        public ParkTransition(BaseState parkState, CarAI carAI)
        {
            this.parkState = parkState as ParkState;
            this.carAI = carAI;
            nearest = RoadWaypointsHelper.FindNearestWayToGraph(carAI.parkPosition);
            nearest = carAI.parkPosition + (nearest - carAI.parkPosition).normalized * 2.5f;
            orientation = carAI.parkPosition - nearest;
        }

        public BaseState GetNextState()
        {
            return parkState;
        }

        public bool IsValid()
        {
            float angle = Vector2.Angle(carAI.transform.right, -orientation);
            Debug.Log(angle);
            if (carAI.CheckObscuring().collider != null)
            {
                //Debug.Log("is obscure");
                return true;
            }

            if (angle < 60)
            {
                //Debug.Log("angle 60");
                return true;
            }

            if (Vector2.Distance(carAI.transform.position, nearest) > 1.5f)
            {
                return true;
            }

            return false;
        }

        public void OnTransition()
        {
            carAI.ChangeDestination();
        }
    }
}
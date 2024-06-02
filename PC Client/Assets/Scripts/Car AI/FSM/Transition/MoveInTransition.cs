using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Carpark.AI.Agent;
using Carpark.AI.Waypoint;

namespace Carpark.AI.FSM
{
    public class MoveInTransition : ITransition
    {
        private ParentState inState;
        private CarAI carAI;
        private float timeOut;

        public MoveInTransition(BaseState inState, CarAI carAI)
        {
            this.inState = inState as ParentState;
            this.carAI = carAI;
            timeOut = Random.Range(10, 20);
        }

        public BaseState GetNextState()
        {
            return inState;
        }

        public bool IsValid()
        {
            return Vector2.Distance(RoadWaypoints.Instance.OutDestination.position, carAI.transform.position) < 0.1f;
        }

        public void OnTransition()
        {
            timeOut = Random.Range(10, 20);
            carAI.transform.position = RoadWaypoints.Instance.InGate.position;
            carAI.transform.eulerAngles = new Vector3(0, 0, 180);
            carAI.GetComponent<CarController>().Init();
            carAI.GetPathInParkingLot();
            carAI.destination = carAI.Path[0];
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carpark.AI.Waypoint;
using DG.Tweening;
using Carpark.AI.FSM;

namespace Carpark.AI.Agent
{
    public class CarAI : MonoBehaviour
    {
        private CarController m_controller;
        [HideInInspector] public Vector3 destination;
        [HideInInspector] public Vector3 milestone;
        [HideInInspector] public List<Vector3> Path;

        private bool isChangingDestination = false;
        private bool isReversing = false;

        [HideInInspector] public Vector3 parkPosition;

        public FSM.FSM FSM;

        // Start is called before the first frame update
        void Start()
        {
            m_controller = GetComponent<CarController>();
            parkPosition = transform.position;

            //GetPathOutTParkingLot();
            var inState = new ParentState(m_controller);
            var idleState = new IdleState(m_controller);
            var outState = new ParentState(m_controller);
            inState.FSM = GetFSMMoveInParkingLot();
            outState.FSM = GetFSMMoveOutParkingLot();
            outState.Transitions.Add(new MoveInTransition(inState, this));
            inState.Transitions.Add(new ToIdleTransition(idleState, this));
            idleState.Transitions.Add(new MoveOutTransition(outState, this));


            List<BaseState> states = new List<BaseState>() { outState, idleState, inState };
            FSM = new FSM.FSM(states);
            GetPathOutParkingLot();
            //transform.position = RoadWaypoints.Instance.InGate.position;
            //transform.eulerAngles = new Vector3(0, 0, 180);
            destination = Path[0];
            FSM.Init();
        }

        private FSM.FSM GetFSMMoveInParkingLot()
        {
            var sprintState = new SprintState(m_controller);
            var dribbleState = new DribbleState(m_controller);
            var stopState = new StopState(m_controller);
            var parkState = new ParkState(m_controller);

            sprintState.Transitions.Add(new DribbleTransition(dribbleState, this));
            dribbleState.Transitions.Add(new SprintTransition(sprintState, this));
            dribbleState.Transitions.Add(new StopTransition(stopState, this));
            sprintState.Transitions.Add(new StopTransition(stopState, this));
            stopState.Transitions.Add(new ParkTransition(parkState, this));

            List<BaseState> states = new List<BaseState>() { sprintState, dribbleState, stopState, parkState };
            var inFSM = new FSM.FSM(states);
            return inFSM;
        }

        private FSM.FSM GetFSMMoveOutParkingLot()
        {
            var sprintState = new SprintState(m_controller);
            var dribbleState = new DribbleState(m_controller);
            var perpendicularOut = new PerpendicularOutState(m_controller);

            perpendicularOut.Transitions.Add(new PerpendicularOutExitTransition(sprintState, this));
            sprintState.Transitions.Add(new DribbleTransition(dribbleState, this));
            dribbleState.Transitions.Add(new SprintTransition(sprintState, this));
            List<BaseState> states = new List<BaseState>() {perpendicularOut, sprintState, dribbleState };
            var outFSM = new FSM.FSM(states);
            return outFSM;
        }

        private void Update()
        {
            FSM.Update();
        }

        public void GetPathOutParkingLot()
        {
            Path = new List<Vector3>();
            Segment currentEdge;
            Path.Add(FindNearestWayToGraph(out currentEdge));

            var wayponts = RoadWaypoints.Instance.BFS(currentEdge.end, RoadWaypoints.Instance.OutDestination);
            for (int i = 0; i < wayponts.Count; i++)
            {
                Path.Add(wayponts[i].position);
            }
        }

        public void GetPathInParkingLot()
        {
            Path = new List<Vector3>();
            Segment edge;
            Vector3 nearest = FindNearestWayToGraph(out edge);
            var waypoints = RoadWaypoints.Instance.BFS(RoadWaypoints.Instance.InGate, edge.begin);
            for (int i = 0; i < waypoints.Count; i++)
            {
                Debug.Log(waypoints[i]);
                Path.Add(waypoints[i].position);
            }
            var direction = (nearest - parkPosition).normalized;
            var middle = parkPosition + direction * 2.5f + (Path[Path.Count - 1] - nearest);
            Path[Path.Count - 1] = middle;
            //Path.Add(middle);
            Path.Add(parkPosition + direction * 2.5f);
        }

        private Vector3 FindNearestWayToGraph(out Segment edge)
        {
            float currentDistance = Mathf.Infinity;
            Vector3 pos = Vector3.zero;
            var edges = RoadWaypoints.Instance.Edges;
            edge = edges[0];
            for (int i = 0; i < edges.Length; i++)
            {
                var newpos = RoadWaypointsHelper.FindNearestPointOnLine(edges[i], parkPosition);
                float sqrDistance = Vector2.SqrMagnitude(newpos - parkPosition);
                if (sqrDistance < currentDistance)
                {
                    pos = newpos;
                    currentDistance = sqrDistance;
                    edge = edges[i];
                }
            }
            //pos = RoadWaypointsHelper.FindNearestPointOnLine(edges[0], transform.position);
            return pos;
        }

        public void ChangeDestination()
        {
            milestone = destination;
            if (Path.Count == 0)
                return;
            Path.RemoveAt(0);
            if (Path.Count == 0)
                return;
            destination = Path[0];
            //DOVirtual.DelayedCall(1, () => isChangingDestination = false);
        }

        public bool ReachFinalDestination()
        {
            return destination == Path[Path.Count - 1];
        }    

        private void Reverse()
        {
            m_controller.fwMode = -m_controller.fwMode;
            isReversing = false;
        }

        public RaycastHit2D CheckObscuring(float length = 1)
        {
            Physics2D.queriesHitTriggers = true;
            Debug.DrawRay(transform.position + transform.up * 0.3f + m_controller.fwMode * transform.right * 1.5f, m_controller.fwMode * transform.right * length, Color.red);
            Debug.DrawRay(transform.position - transform.up * 0.3f + m_controller.fwMode * transform.right * 1.5f, m_controller.fwMode * transform.right * length, Color.red);
            var right = Physics2D.Raycast(transform.position + transform.up * 0.3f + m_controller.fwMode * transform.right * 1.5f,
                                          m_controller.fwMode * transform.right * 2,
                                          length,
                                          LayerMask.GetMask("Cars", "Human", "Default", "Environment"));
            var left = Physics2D.Raycast(transform.position - transform.up * 0.3f + m_controller.fwMode * transform.right * 1.5f,
                                         m_controller.fwMode * transform.right * 2,
                                         length,
                                         LayerMask.GetMask("Cars", "Human", "Default", "Environment"));

            if (right.collider == null && left.collider == null)
            {
                return right;
            }
            else if (right.collider == null)
            {
                return left;
            }
            else if (left.collider == null)
            {
                return right;
            }
            else
            {
                Vector2 pos = transform.position;
                float rightDistance = Vector2.SqrMagnitude(right.point - pos);
                float leftDistance = Vector2.SqrMagnitude(left.point - pos);
                return rightDistance < leftDistance ? right : left;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(destination, 1);
        }
    }

}


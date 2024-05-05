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
        private List<Vector3> path;

        private bool isChangingDestination = false;
        private bool isReversing = false;

        private Vector3 parkPosition;

        public FSM.FSM FSM;

        // Start is called before the first frame update
        void Start()
        {
            m_controller = GetComponent<CarController>();

            var sprintState = new SprintState(m_controller);
            var dribbleState = new DribbleState(m_controller);

            sprintState.Transitions.Add(new DribbleTransition(dribbleState, this));
            dribbleState.Transitions.Add(new SprintTransition(sprintState, this));
            List<BaseState> states = new List<BaseState>() { sprintState, dribbleState };
            FSM = new FSM.FSM(states);
            parkPosition = transform.position;
            //GetPathOutTParkingLot();
            GetPathInParkingLot();
            transform.position = RoadWaypoints.Instance.InGate.position;
            destination = path[0];
        }

        //private void FixedUpdate()
        //{
        //    MoveTowardDestination(destination);
        //}

        private void Update()
        {
            FSM.Update();
        }

        private void GetPathOutTParkingLot()
        {
            path = new List<Vector3>();
            Segment currentEdge;
            path.Add(FindNearestWayToGraph(out currentEdge));

            var wayponts = RoadWaypoints.Instance.BFS(currentEdge.end, RoadWaypoints.Instance.OutDestination);
            for (int i = 0; i < wayponts.Count; i++)
            {
                path.Add(wayponts[i].position);
            }
        }

        private void GetPathInParkingLot()
        {
            path = new List<Vector3>();
            Segment edge;
            Vector3 nearest = FindNearestWayToGraph(out edge);
            var waypoints = RoadWaypoints.Instance.BFS(RoadWaypoints.Instance.InGate, edge.begin);
            for (int i = 0; i < waypoints.Count; i++)
            {
                path.Add(waypoints[i].position);
            }
            path.Add(nearest);
            path.Add(parkPosition);
        }

        private Vector3 FindNearestWayToGraph(out Segment edge)
        {
            float currentDistance = Mathf.Infinity;
            Vector3 pos = Vector3.zero;
            var edges = RoadWaypoints.Instance.Edges;
            edge = edges[0];
            for (int i = 0; i < edges.Length; i++)
            {
                var newpos = RoadWaypointsHelper.FindNearestPointOnLine(edges[i], transform.position);
                float sqrDistance = Vector2.SqrMagnitude(newpos - transform.position);
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

        private void MoveTowardDestination(Vector3 destination)
        {
            if (CheckObscuring().collider != null)
            {
                Debug.Log("Stop");
                m_controller.Brake();
                if (!isReversing)
                {
                    isReversing = true;
                    DOVirtual.DelayedCall(1f, Reverse).OnComplete(() => DOVirtual.DelayedCall(2f, Reverse));
                }
                return;
            }

            Vector2 direction = destination - transform.position;
            float angle = Vector2.SignedAngle(transform.right, direction);
            isChangingDestination = angle > 3 || angle < -3;
            m_controller.Steer(angle);

            if (Vector2.SqrMagnitude(transform.position - destination) <= 10f)
            {
                m_controller.Brake();
                if (!isChangingDestination)
                {
                    ChangeDestination();
                    isChangingDestination = true;
                }
                return;
            }
            else
            {
                if (!isChangingDestination)
                {
                    m_controller.Throtte();
                }
                else
                {
                    if (m_controller.Velocity < 1f)
                        m_controller.Throtte();
                    else
                        m_controller.Brake();
                }
            }

            //transform.right = Vector3.Normalize(destination - transform.position);
            m_controller.Throtte();
        }

        public void ChangeDestination()
        {
            milestone = destination;
            if (path.Count == 0)
                return;
            path.RemoveAt(0);
            if (path.Count == 0)
                return;
            destination = path[0];
            //DOVirtual.DelayedCall(1, () => isChangingDestination = false);
        }

        private void Reverse()
        {
            m_controller.fwMode = -m_controller.fwMode;
            isReversing = false;
        }

        public RaycastHit2D CheckObscuring()
        {
            Debug.DrawRay(transform.position + transform.up * 0.27f + m_controller.fwMode * transform.right * 1.5f, m_controller.fwMode * transform.right * 2, Color.red);
            Debug.DrawRay(transform.position - transform.up * 0.27f + m_controller.fwMode * transform.right * 1.5f, m_controller.fwMode * transform.right * 2, Color.red);
            var right = Physics2D.Raycast(transform.position + transform.up * 0.27f + m_controller.fwMode * transform.right * 1.5f,
                                          m_controller.fwMode * transform.right * 2,
                                          1,
                                          LayerMask.GetMask("Cars", "Human", "Default"));
            var left = Physics2D.Raycast(transform.position - transform.up * 0.27f + m_controller.fwMode * transform.right * 1.5f,
                                         m_controller.fwMode * transform.right * 2,
                                         1,
                                         LayerMask.GetMask("Cars", "Human", "Default"));

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


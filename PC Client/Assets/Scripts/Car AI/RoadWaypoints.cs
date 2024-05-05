using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Carpark.AI.Waypoint
{
    public class RoadWaypoints : MonoBehaviour
    {
        public Transform[] Waypoints;
        public Segment[] Edges;

        public Transform OutDestination;
        public Transform InGate;

        public static RoadWaypoints Instance;

        private Dictionary<Transform, Transform> comeFrom;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < Edges.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(Edges[i].begin.position, Edges[i].end.position);
            }
        }

        public bool AStar(Transform begin, Transform end)
        {
            comeFrom = new Dictionary<Transform, Transform>();
            //if (startID == endID) {

            //    pathList.Clear();
            //    return false;
            //}

            if (begin == null || end == null) return false;

            List<Transform> open = new List<Transform>();
            List<Transform> closed = new List<Transform>();

            //float tentative_g_score = 0.0f;
            //bool tentative_is_better;

            //start.g = 0.0f;
            //start.h = distance(start, end);
            //start.f = start.h;

            open.Add(begin);

            while (open.Count > 0)
            {
                int i = 0;
                Transform thisNode = open[i];
                if (thisNode == end)
                {
                    ReconstructPath(begin, end);
                    return true;
                }

                open.RemoveAt(i);
                closed.Add(thisNode);
                Transform neighbour;
                foreach (Segment e in Edges)
                {
                    if (e.begin == thisNode)
                    {
                        neighbour = e.end;

                        if (closed.IndexOf(neighbour) > -1) continue;

                        //tentative_g_score = thisNode.g + distance(thisNode, neighbour);
                        if (open.IndexOf(neighbour) == -1)
                        {
                            open.Add(neighbour);
                            comeFrom[neighbour] = thisNode;
                            Debug.Log(thisNode.ToString() + neighbour.ToString());
                            //tentative_is_better = true;
                        }
                        //else if (tentative_g_score < neighbour.g)
                        //{

                        //    tentative_is_better = true;
                        //}
                        //else
                        //    tentative_is_better = false;

                        //if (tentative_is_better)
                        //{

                        //    neighbour.cameFrom = thisNode;
                        //    neighbour.g = tentative_g_score;
                        //    neighbour.h = distance(thisNode, end);
                        //    neighbour.f = neighbour.g + neighbour.h;
                        //}
                    }
                }
            }
            return false;
        }

        private List<Transform> ReconstructPath(Transform startId, Transform endId)
        {
            List<Transform> pathList = new List<Transform>();
            pathList.Clear();
            pathList.Add(endId);

            var p = comeFrom[endId];

            while (p != startId && p != null)
            {

                pathList.Insert(0, p);
                p = comeFrom[p];
            }

            pathList.Insert(0, startId);

            //for (int i = 0; i < pathList.Count; i++)
            //    Debug.Log(pathList[i]);
            return pathList;
        }

        public List<Transform> BFS(Transform begin, Transform end)
        {
            comeFrom = new Dictionary<Transform, Transform>();
            List<Transform> open = new List<Transform>();
            List<Transform> closed = new List<Transform>();

            //float tentative_g_score = 0.0f;
            //bool tentative_is_better;

            //start.g = 0.0f;
            //start.h = distance(start, end);
            //start.f = start.h;

            open.Add(begin);

            while (open.Count > 0)
            {
                int i = 0;
                Transform thisNode = open[i];
                if (thisNode == end)
                {
                    return ReconstructPath(begin, end);
                }

                open.RemoveAt(i);
                closed.Add(thisNode);
                Transform neighbour;
                foreach (Segment e in Edges)
                {
                    if (e.begin == thisNode)
                    {
                        neighbour = e.end;

                        if (closed.IndexOf(neighbour) > -1) continue;

                        //tentative_g_score = thisNode.g + distance(thisNode, neighbour);
                        if (open.IndexOf(neighbour) == -1)
                        {
                            open.Add(neighbour);
                            comeFrom[neighbour] = thisNode;
                            Debug.Log(thisNode.ToString() + neighbour.ToString());
                            //tentative_is_better = true;
                        }
                        //else if (tentative_g_score < neighbour.g)
                        //{

                        //    tentative_is_better = true;
                        //}
                        //else
                        //    tentative_is_better = false;

                        //if (tentative_is_better)
                        //{

                        //    neighbour.cameFrom = thisNode;
                        //    neighbour.g = tentative_g_score;
                        //    neighbour.h = distance(thisNode, end);
                        //    neighbour.f = neighbour.g + neighbour.h;
                        //}
                    }
                }
            }
            return new List<Transform>();
        }    
    }
    
    [System.Serializable]
    public struct Segment
    {
        public Transform begin;
        public Transform end;
    }
}

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

        public static RoadWaypoints Instance;

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

        public List<Transform> BFS(Transform begin, Transform end)
        {
            List<Transform> path = new List<Transform>();
            List<Transform> frontier = new List<Transform>();
            List<Transform> explored = new List<Transform>();

            Dictionary<Transform, Transform> parents = new Dictionary<Transform, Transform>();

            frontier.Add(begin);
            while(true)
            {
                if (frontier.Count == 0)
                {
                    Debug.Log("No path");
                    return path;
                }
                var current = frontier[0];
                if (current == end)
                {
                    while (true)
                    {
                        path.Add(current);
                        if (current == begin)
                            break;
                        if (parents.ContainsKey(current))
                        {
                            current = parents[current];
                        }
                    }
                    path.Reverse();
                    Debug.Log(path);
                    return path;

                }

                //explore current
                for (int i = 0; i < Edges.Length; i++)
                {
                    if (Edges[i].begin == current && explored.Contains(Edges[i].end))
                    {
                        frontier.Add(Edges[i].end);
                        Debug.Log(Edges[i].end);
                        parents[Edges[i].end] = current;
                    }
                }
                frontier.Remove(current);
                explored.Add(current);
            }
        }    
    }
    
    [System.Serializable]
    public struct Segment
    {
        public Transform begin;
        public Transform end;
    }
}

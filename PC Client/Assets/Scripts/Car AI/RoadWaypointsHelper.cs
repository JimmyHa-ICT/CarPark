using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carpark.AI.Waypoint
{
    public class RoadWaypointsHelper
    {
        public static Vector3 FindNearestPointOnLine(Segment edge, Vector2 point)
        {
            //Get heading as a vector
            Vector3 lineHeadingVector = (edge.end.position - edge.begin.position);

            //Store the max distance
            float maxDistance = lineHeadingVector.magnitude;
            lineHeadingVector.Normalize();

            //Do projection from the start position to the point
            Vector3 lineVectorStartToPoint = point - (Vector2) edge.begin.position;
            float dotProduct = Vector2.Dot(lineVectorStartToPoint, lineHeadingVector);

            //Clamp the dot product to maxDistance
            dotProduct = Mathf.Clamp(dotProduct, 0f, maxDistance);

            return edge.begin.position + lineHeadingVector * dotProduct;
        }
    }
}

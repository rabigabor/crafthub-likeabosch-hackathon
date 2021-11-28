using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSimulatorHelperScript : SimulatorHelper
{

    GameObject sphere1, sphere2, sphere3, sphere4;
    void Start()
    {

        // GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // GameObject sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // GameObject sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // GameObject sphere4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // sphere1.transform.localScale = Vector3.one*0.05f;
        // sphere2.transform.localScale = Vector3.one*0.05f;
        // sphere3.transform.localScale = Vector3.one*0.05f;
        // sphere4.transform.localScale = Vector3.one*0.05f;
    }
    private Vector3[] getCorners()
    {   
        Bounds bounds = transform.GetComponent<MeshFilter>().mesh.bounds;

        
        var radius = (bounds.max.x - bounds.min.x)/2;
        Debug.Log(radius);
        return new Vector3[] {
            transform.TransformPoint(new Vector3(radius, 0, -radius)),
            transform.TransformPoint(new Vector3(-radius, 0, -radius)),
            transform.TransformPoint(new Vector3(-radius, 0, radius)),
            transform.TransformPoint(new Vector3(radius, 0, radius))
        };
    }

    private void OnDrawGizmos() {
        // var corners = getCorners();
        // var indices = getIndices();
        // for(var i = 0; i < indices.Length/2; i++){
        //     Debug.DrawLine(corners[indices[i,0]], corners[indices[i,1]]);
        // }
    }

    private int[,] getIndices()
    {
        return new int[,]{
            {0,1},
            {1,2},
            {2,3},
            {3,0}
        };
    }

    public override Vector3 GetDirectIntersectionPoint(Vector3 sensorPosition, Vector3 sensorForward)
    {


        Vector3[] corners = getCorners();
        float minDist = Mathf.Infinity;
        Vector3 nearestIntersection = Vector3.zero;

        int[,] indices = getIndices();


        for (var i = 0; i < indices.Length / 2; i++)
        {
            Vector3 startCorner = corners[indices[i, 0]];
            Vector3 endCorner = corners[indices[i, 1]];


            Vector3 closestPoint = ClosestPointOnLine(startCorner, endCorner, sensorPosition);

            // Debug.DrawLine(closestPoint, sensorPosition);
            if (
                isBetweenSegment(closestPoint, startCorner, endCorner) &&
                isAngleInView(sensorPosition, sensorForward, closestPoint)
                )
            {
                float currentDistance = Vector3.Distance(closestPoint, sensorPosition);
                if (currentDistance < minDist && currentDistance <= Helper.MAX_DISTANCE)
                {
                    minDist = currentDistance;
                    // Debug.Log("minDist " + minDist);
                    nearestIntersection = closestPoint;
                }
            }
        }

        return nearestIntersection;

    }



    public override Vector3 GetCrossIntersectionPoint(Vector3 sensor1Position, Vector3 sensor1Forward, Vector3 sensor2Position, Vector3 sensor2Forward)
    {
        Vector3[] corners = getCorners();
        float minDist = Mathf.Infinity;
        Vector3 nearestIntersection = Vector3.zero;

        int[,] indices = getIndices();


        for (var i = 0; i < indices.Length / 2; i++)
        {
            Vector3 startCorner = corners[indices[i, 0]];
            Vector3 endCorner = corners[indices[i, 1]];


            Vector3 closestPoint1 = ClosestPointOnLine(startCorner, endCorner, sensor1Position);

            Vector3 closestPoint2 = ClosestPointOnLine(startCorner, endCorner, sensor2Position);



            // sphere1.transform.position = closestPoint1;
            // sphere2.transform.position = closestPoint1;

            float X = Vector3.Distance(sensor1Position, sensor2Position);
            float Z = Vector3.Distance(sensor1Position, closestPoint1);
            float Y = Vector3.Distance(sensor2Position, closestPoint2);

            float f2 = (X * X + 4 * Y * Z) / (Y * Y / (Z * Z) + 1 + 2 * Y / Z);
            if (f2 > 0)
            {
                float f = Mathf.Sqrt(f2);
                float angle = Mathf.Rad2Deg * Mathf.Acos(Z / f);

                if (Vector3.Cross(sensor1Position-transform.position, sensor2Position-transform.position).y > 0)
                {
                    angle *= -1;
                }

                Vector3 intersection = sensor1Position + (Quaternion.AngleAxis(-angle + 90, Vector3.down) * (startCorner - endCorner)).normalized * f;

                if (
                    isBetweenSegment(intersection, startCorner, endCorner) &&
                    isAngleInView(sensor1Position, sensor1Forward, intersection) &&
                    isAngleInView(sensor2Position, sensor2Forward, intersection)
                )
                {
                    float currentDistance = Vector3.Distance(intersection, sensor1Position);
                    if (currentDistance < minDist && currentDistance <= Helper.MAX_DISTANCE)
                    {
                        minDist = currentDistance;
                        // Debug.Log("minDist " + minDist);
                        nearestIntersection = intersection;
                    }
                }
            }
        }
        return nearestIntersection;
    }

}

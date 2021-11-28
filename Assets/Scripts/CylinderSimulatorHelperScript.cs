using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSimulatorHelperScript : SimulatorHelper
{


    public override Vector3 GetDirectIntersectionPoint(Vector3 sensorPosition, Vector3 sensorForward)
    {
        
        Bounds bounds = GetComponent<MeshRenderer>().bounds;

        float radius = (bounds.max.x-bounds.min.x)/2; 

        Vector2 vec = (xz(sensorPosition) - xz(transform.position)).normalized * radius + xz(transform.position);


        Vector3 intersection = new Vector3(vec.x, sensorPosition.y, vec.y);

        if (
            isAngleInView(sensorPosition, sensorForward, intersection) &&
            Vector3.Distance(intersection, sensorPosition) <= Helper.MAX_DISTANCE
        )
        {
            return intersection;
        }
        else
        {
            return Vector3.zero;
        }
    }


    public override Vector3 GetCrossIntersectionPoint(Vector3 sensor1Position, Vector3 sensor1Forward, Vector3 sensor2Position, Vector3 sensor2Forward)
    {
        Vector3 center = (sensor1Position + sensor2Position) / 2;
        Vector3 intersection = GetDirectIntersectionPoint(center, sensor1Forward);
        if (
            isAngleInView(sensor1Position, sensor1Forward, intersection) &&
            isAngleInView(sensor2Position, sensor2Forward, intersection) &&
            Vector3.Distance(intersection, sensor1Position) <= Helper.MAX_DISTANCE
        )
        {
            return intersection;
        }
        else
        {
            return Vector3.zero;
        }
    }
}

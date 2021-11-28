using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public const float MAX_DISTANCE = 3.0f;


    // public static bool GetIntersectionPointCoordinates(out Vector3 intersection, Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2)
    // {
    //     float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);

    //     if (tmp == 0)
    //     {
    //         intersection = Vector3.zero;
    //         return false;
    //     }

    //     float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;


    //     intersection = new Vector3(
    //         B1.x + (B2.x - B1.x) * mu,
    //         0,
    //         B1.y + (B2.y - B1.y) * mu
    //     );
    //     return true;
    // }


}


public abstract class SimulatorHelper : MonoBehaviour
{

    private float angleOfView = 120;
    public abstract Vector3 GetDirectIntersectionPoint(Vector3 sensorPosition, Vector3 sensorForward);
    public abstract Vector3 GetCrossIntersectionPoint(Vector3 sensor1Position, Vector3 sensor1Forward, Vector3 sensor2Position, Vector3 sensor2Forward);

    public bool isBetweenSegment(Vector3 point, Vector3 start, Vector3 end)
    {
        return (
            point.x >= Mathf.Min(start.x, end.x) &&
            point.x <= Mathf.Max(start.x, end.x) &&
            point.z >= Mathf.Min(start.z, end.z) &&
            point.z <= Mathf.Max(start.z, end.z)
        );
    }

    public static Vector2 xz(Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }


    public Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
    {
        var vVector1 = vPoint - vA;
        var vVector2 = (vB - vA).normalized;

        var d = Vector3.Distance(vA, vB);
        var t = Vector3.Dot(vVector2, vVector1);
        var vVector3 = vVector2 * t;

        var vClosestPoint = vA + vVector3;

        return new Vector3(vClosestPoint.x, vPoint.y, vClosestPoint.z);
    }

    public bool isAngleInView(Vector3 sensorPosition, Vector3 sensorForward, Vector3 intersection)
    {
        float angle = Vector3.Angle(sensorForward, intersection - sensorPosition);
        return angle >= (90 - angleOfView / 2) && angle <= (90 + angleOfView / 2) && Vector3.Cross(sensorForward, intersection - sensorPosition).y <= 0;
    }

}
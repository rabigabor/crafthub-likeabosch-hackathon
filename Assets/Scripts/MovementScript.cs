using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public float speed = 0;
    public float rotation = 0;
    public float prevRotation = 0;
    public GameObject[] wheels;
    // Start is called before the first frame update



    private void OnDrawGizmos()
    {
        // foreach (GameObject wheel in wheels)
        // {
        //     Gizmos.DrawRay(wheel.transform.position, wheel.transform.right);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject wheel in wheels)
        {
            float rotationSpeed = speed / 0.4f * Mathf.Rad2Deg;
            // if (wheel.name.Contains("Wheel_f"))
            // {

            //     wheel.transform.rotation = (
            //         Quaternion.AngleAxis(rotation - prevRotation, Vector3.down) *
            //         Quaternion.AngleAxis(Time.deltaTime * rotationSpeed, wheel.transform.right)
            //         * wheel.transform.rotation
            //     );
            // }
            // else
            // {

            wheel.transform.rotation = (
                Quaternion.AngleAxis(Time.deltaTime * rotationSpeed, wheel.transform.right)
                * wheel.transform.rotation
            );
            // }
        }

        // Vector3 intersection;

        // Vector3 frontPos = (wheels[0].transform.position + wheels[1].transform.position) / 2;
        // Vector3 rearPos = (wheels[2].transform.position + wheels[3].transform.position) / 2;

        // Debug.DrawRay(frontPos, wheels[0].transform.right, Color.red);
        // Debug.DrawRay(rearPos, wheels[2].transform.right, Color.red);
        // float angle = Vector2.SignedAngle(SimulatorHelper.xz(wheels[0].transform.right), SimulatorHelper.xz(wheels[2].transform.right));


        // float radius = Vector3.Distance(frontPos, rearPos) / 2 * Mathf.Sin(angle / 2);
        // if (radius > 0)
        // {

        //     float angleToTurn = 360 * Time.deltaTime / (2 * radius * Mathf.PI);
        //     Debug.Log(radius + " " + angleToTurn);
        //     transform.rotation = transform.rotation * Quaternion.AngleAxis(-angleToTurn * speed, Vector3.down);
        // }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}

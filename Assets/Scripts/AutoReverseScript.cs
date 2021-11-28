using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoReverseScript : MonoBehaviour
{
    // Start is called before the first frame update
    const float MAX_SPEED = 3f;

    const float MIN_DISTANCE = 0.35f;

    // const float MAX_ROTATION = 30f;
    const float acceleration = 2f;

    const float rotationSpeed = 30f;

    private float startTime;

    MovementScript ms;

    private Vector3 targetPosition;
    private Vector3 startPosition;

    private Vector3 sensorOffset = new Vector3(0, 0.7f, -2.644865f);

    bool isStarted = false;

    bool isOverriding = false;
    void Start()
    {
        ms = GetComponent<MovementScript>();
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, 0, -10) + sensorOffset;
    }


    public void handleSmallestDetection(float smallestDetection)
    {
        if (isStarted && Mathf.Min(smallestDetection, Vector3.Distance(transform.position, targetPosition)) < (ms.speed * ms.speed / 2 / acceleration + MIN_DISTANCE))
        {
            isOverriding = true;

            Debug.Log("MOSTMOSTMOST");
            // Vector3 currentSpeed = transform.forward * ms.speed;

            ms.speed = Mathf.Clamp(ms.speed + acceleration * Time.deltaTime, -MAX_SPEED, 0);
            // targetPosition = transform.position + sensorOffset - transform.forward * smallestDetection;
        }
        Debug.DrawLine(transform.position, targetPosition, Color.cyan);
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawSphere(targetPosition, 0.5f);

        // Gizmos.DrawRay(transform.position + sensorOffset, -transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isStarted = true;
            isOverriding = false;
            audioSource.Play();
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.mute = !audioSource.mute;
        }

        if (!isOverriding)
        {
            if (isStarted)
            {

                ms.speed = Mathf.Clamp(ms.speed + acceleration * Time.deltaTime * -1, -MAX_SPEED, MAX_SPEED);
            }
            else
            {

                float vertical = 0;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    vertical = 1;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    vertical = -1;
                }

                ms.speed = Mathf.Clamp(ms.speed + acceleration * Time.deltaTime * vertical, -MAX_SPEED, 0);
            }
        }
        else
        {

            // Vector3 newPosition = new Vector3(
            //     transform.position.x,
            //     transform.position.y,
            //     Mathf.Clamp(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime).z - transform.position.z, 0, MAX_SPEED * Time.deltaTime)
            // );

            // ms.speed = 0;
            // transform.position = newPosition;
        }


        // float horizontal = 0;
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     horizontal = 1;
        // }
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     horizontal = -1;
        // }
        // ms.prevRotation = ms.rotation;
        // ms.rotation = Mathf.Clamp(ms.rotation + rotationSpeed * Time.deltaTime * horizontal, -MAX_ROTATION, MAX_ROTATION);


        if (isStarted)
        {
            // Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref ms.speed, 1f, 3f);
            // transform.position = newPosition;
        }
    }
}

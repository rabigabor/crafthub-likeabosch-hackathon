using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorScript : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject linePrefab;

    public GameObject car;

    List<GameObject> lineRenderers = new List<GameObject>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] sensors = GameObject.FindGameObjectsWithTag("Sensor");
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        // Debug.Log(sensors.Length + " vs " + obstacles.Length);

        foreach (GameObject lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer);
        }


        List<float> allDetections = new List<float>();
        foreach (GameObject sensor1 in sensors)
        {
            List<float> detections = new List<float>();

            foreach (GameObject obstacle in obstacles)
            {
                Vector3 directIntersection = obstacle.GetComponent<SimulatorHelper>().GetDirectIntersectionPoint(sensor1.transform.position, sensor1.transform.forward);

                if (!directIntersection.Equals(Vector3.zero))
                {
                    DrawLineDirect(sensor1.transform.position, directIntersection, Color.cyan);
                    float detection = 2 * Vector3.Distance(directIntersection, sensor1.transform.position);
                    detections.Add(detection);
                    allDetections.Add(detection);
                }
                foreach (GameObject sensor2 in sensor1.GetComponent<SensorScript>().GetNeighbourSensors())
                {
                    if (sensor1.Equals(sensor2)) continue;


                    Vector3 crossIntersection = obstacle.GetComponent<SimulatorHelper>().GetCrossIntersectionPoint(
                        sensor1.transform.position,
                        sensor1.transform.forward,
                        sensor2.transform.position,
                        sensor2.transform.forward
                    );

                    if (!crossIntersection.Equals(Vector3.zero))
                    {
                        DrawLineCross(sensor1.transform.position, crossIntersection, sensor2.transform.position, Color.magenta);

                        float detection = Vector3.Distance(directIntersection, sensor1.transform.position) +
                            Vector3.Distance(directIntersection, sensor2.transform.position);
                        detections.Add(detection);
                        allDetections.Add(detection);
                    }
                }

            }
            sensor1.GetComponent<SensorScript>().handleDetections(detections);
        }

        float smallestDetection = Mathf.Min(allDetections.ToArray()) / 2;
        if(allDetections.Count == 0) smallestDetection = Mathf.Infinity;
        handleAudioForDetection(smallestDetection);
        car.GetComponent<AutoReverseScript>().handleSmallestDetection(smallestDetection);
    }

    void handleAudioForDetection(float smallestDetection)
    {
        // Debug.Log(smallestDetection);
        float level0 = Helper.MAX_DISTANCE * 0.8f;
        float level1 = Helper.MAX_DISTANCE / 3;
        float level2 = Helper.MAX_DISTANCE / 6;

        if (smallestDetection > level0)
        {
            GetComponent<SoundControllerScript>().lastClipName = "";

        }
        else if (smallestDetection > level1)
        {
            GetComponent<SoundControllerScript>().lastClipName = "ATTENTION";
        }
        else if (smallestDetection > level2)
        {
            GetComponent<SoundControllerScript>().lastClipName = "WARNING";
        }
        else
        {
            GetComponent<SoundControllerScript>().lastClipName = "STOP";
        }
    }

    void DrawLineDirect(Vector3 start, Vector3 end, Color color)
    {

        GameObject lineObj = Instantiate(linePrefab);
        LineRenderer lineRend = lineObj.GetComponent<LineRenderer>();
        lineRend.positionCount = 3;
        lineRend.SetPosition(0, start);
        lineRend.SetPosition(1, end);
        lineRend.SetPosition(2, start);
        lineRenderers.Add(lineObj);
        lineRend.startColor = color;
        lineRend.endColor = new Color(color.r, color.g, color.b, 0.2f);
        // lineRend.endColor
        // Destroy(lineObj, Time.deltaTime*2);
    }
    void DrawLineCross(Vector3 start, Vector3 middle, Vector3 end, Color color)
    {

        GameObject lineObj = Instantiate(linePrefab);
        LineRenderer lineRend = lineObj.GetComponent<LineRenderer>();
        lineRend.positionCount = 3;
        lineRend.SetPosition(0, start);
        lineRend.SetPosition(1, middle);
        lineRend.SetPosition(2, end);
        lineRenderers.Add(lineObj);
        lineRend.startColor = color;
        lineRend.endColor = new Color(color.r, color.g, color.b, 0.2f);
        // lineRend.endColor
        // Destroy(lineObj, Time.deltaTime*2);
    }
}

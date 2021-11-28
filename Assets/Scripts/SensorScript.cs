using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensorScript : MonoBehaviour
{
    // #2ecc71
    //rgb(39, 174, 96)rgb(230, 126, 34)rgb(192, 57, 43)
    private Color green = new Color(39 / 255f, 174 / 255f, 96 / 255f);
    private Color orange = new Color(230 / 255f, 126 / 255f, 34 / 255f);
    private Color red = new Color(192 / 255f, 57 / 255f, 43 / 255f);
    public GameObject button;
    public GameObject[] neighbours;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject[] GetNeighbourSensors()
    {
        return neighbours;
    }

    public void handleDetections(List<float> detections)
    {
        if (detections.Count == 0)
        {
            button.GetComponent<Image>().fillAmount = 0;
            button.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        }
        else
        {
            float closest = Mathf.Min(detections.ToArray()) / 2;

            Image image = button.GetComponent<Image>();

            float level0 = Helper.MAX_DISTANCE * 0.8f;
            float level1 = Helper.MAX_DISTANCE / 3;
            float level2 = Helper.MAX_DISTANCE / 6;

            if (closest > level0)
            {
                image.color = green;
            }
            else if (closest > level1)
            {
                image.color = Color.Lerp(orange, green, (closest - level1) / (level0 - level1));
            }
            else if (closest > level2)
            {
                image.color = Color.Lerp(red, orange, (closest - level2) / (level1 - level2));
            }
            else
            {
                image.color = red;
            }
            image.fillAmount = 1 - (closest / Helper.MAX_DISTANCE);
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(closest.ToString("F2"));
        }
    }

}

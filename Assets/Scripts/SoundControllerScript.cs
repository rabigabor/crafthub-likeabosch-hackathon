using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{

    public string lastClipName = "";

    public AudioClip stop;
    public AudioClip attention;
    public AudioClip warning;

    private float lastTime;

    public float minTimeBetween = 2f;
    // Update is called once per frame
    void Update()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.mute = !audioSource.mute;
        }
        if (lastClipName.Equals("")) return;

        if (lastTime != 0 && ((Time.time - lastTime) < minTimeBetween)) return;

        switch (lastClipName)
        {
            case "STOP":
                audioSource.PlayOneShot(stop);

                break;
            case "WARNING":
                audioSource.PlayOneShot(warning);
                break;

            case "ATTENTION":
                audioSource.PlayOneShot(attention);
                break;
        }

        // Debug.Log("Played " + lastClipName);



        lastTime = Time.time;
    }
}

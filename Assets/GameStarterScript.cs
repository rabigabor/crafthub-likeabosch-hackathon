using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarterScript : MonoBehaviour
{
    bool isStarted = false;
    public bool isInGame = true;

    // Update is called once per frame
    void Update()
    {
        if(!isInGame && Input.GetKeyDown(KeyCode.Return) && !isStarted){
            StartCoroutine(GetComponent<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "GameScene"));
            isStarted = true;
        }
        if(isInGame && Input.GetKeyDown(KeyCode.Escape) && !isStarted){
            
            StartCoroutine(GetComponent<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "FirstScene"));
            isStarted = true;

        }
    }

}

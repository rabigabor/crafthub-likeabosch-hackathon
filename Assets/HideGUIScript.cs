using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){
            foreach(Transform child in GetComponentInChildren<Transform>()){
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
    }
}

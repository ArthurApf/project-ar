using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    public bool loadScene;
    public bool cameraWaypoint;
    public string sceneToLoad;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "player")
        {
            return;
        }

        if (loadScene && !string.IsNullOrEmpty(sceneToLoad))
        {
            Application.LoadLevel(sceneToLoad);
        }

        if (cameraWaypoint)
        {
            cameraWaypoint = false;
            CameraRail.cameraRail.ChangePoint();
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
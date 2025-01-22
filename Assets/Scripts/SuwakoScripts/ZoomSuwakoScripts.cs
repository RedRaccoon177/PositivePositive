using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomSuwakoScripts : MonoBehaviour
{
    GameObject suwako;
    bool startShow = true;

    void Start()
    {
        suwako = gameObject;
        Camera.main.GetComponent<CameraMove>().ZoomBoss(suwako, startShow);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startShow = false;
            Camera.main.GetComponent<CameraMove>().ZoomBoss(suwako, startShow);
        }
    }
}

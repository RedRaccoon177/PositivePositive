using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormRotation : MonoBehaviour
{
    GameObject zombieInfo;
    public GameObject ZombieInfo
    {
        set
        {
            zombieInfo = value;
        }
    }

    float radius;
    float rotationSpeed;
    float angle;
    void Start()
    {
        radius = 5f;
        rotationSpeed = 50f;
        angle = 0f;
    }

    void Update()
    {
        angle += Time.deltaTime * rotationSpeed;
        angle %= 360;
        float ran = angle * Mathf.Deg2Rad; 
        float x = zombieInfo.transform.position.x + (radius * Mathf.Cos(ran));
        float y = zombieInfo.transform.position.y + (radius * Mathf.Sin(ran));
        transform.position = new Vector3(x, y, 0);
    }

}
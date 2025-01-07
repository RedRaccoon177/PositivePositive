using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(player.transform.position.x, player.transform.position.y, -10), 1);
    }
}

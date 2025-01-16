using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoBottomCheck : MonoBehaviour
{
    SuwakoController suwako;

    private void Start()
    {
        suwako = GetComponentInParent<SuwakoController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground" || collision.tag == "Wall")
        {
            suwako.falling = 2;
        }
    }
}

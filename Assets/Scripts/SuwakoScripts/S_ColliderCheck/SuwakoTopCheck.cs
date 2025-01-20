using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoTopCheck : MonoBehaviour
{
    SuwakoController suwako;
    BoxCollider2D BoxCollider2D;

    private void Start()
    {
        suwako = GetComponentInParent<SuwakoController>();
        BoxCollider2D = GetComponentInParent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Wall")
        {
            suwako.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Wall")
        {
            suwako.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

}

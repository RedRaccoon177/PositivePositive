using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoTopCheck : MonoBehaviour
{
    SuwakoController suwako;

    private void Start()
    {
        suwako = GetComponentInParent<SuwakoController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            suwako.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            suwako.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}

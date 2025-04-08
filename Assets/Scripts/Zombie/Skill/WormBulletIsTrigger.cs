using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBulletIsTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("1");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("2");
            transform.parent.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            Debug.Log("3");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("4");
            transform.parent.GetComponent<Collider2D>().isTrigger = true;
        }
    }
}

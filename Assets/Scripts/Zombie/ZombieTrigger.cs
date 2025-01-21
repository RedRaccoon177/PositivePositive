using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTrigger : MonoBehaviour
{
    public GameObject zombiInfo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.transform.GetComponentInParent<BoxCollider2D>() == null)
            {
                Debug.Log("널널");
            }
            // 이건 안됌
            //gameObject.transform.GetComponentInParent<BoxCollider2D>().isTrigger = true;
            zombiInfo.GetComponent<BoxCollider2D>().isTrigger = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //transform.GetComponentInParent<BoxCollider2D>().isTrigger = true; 
            zombiInfo.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}

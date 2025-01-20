using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTrigger : MonoBehaviour
{
    public GameObject aa;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("트리거 발동");
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.transform.GetComponentInParent<BoxCollider2D>() == null)
            {
                Debug.Log("널널");
            }
            // 이건 못찾아옴
            transform.GetComponentInParent<BoxCollider2D>().isTrigger = true; 
            // 이건 찾아옴
            aa.GetComponentInParent<BoxCollider2D>().isTrigger = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //transform.GetComponentInParent<BoxCollider2D>().isTrigger = true; 
            aa.GetComponentInParent<BoxCollider2D>().isTrigger = false;
        }
    }
}

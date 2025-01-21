using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormRotationController : MonoBehaviour
{
    public ZombieObjectPooling objPooling { get; set; }
    public ZombieController zombieInfo { get; set; }
    //public GameObject zombieii;
    private void Start()
    {
        zombieInfo  = transform.GetComponentInParent<ZombieController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 로프
        if (collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
            zombieInfo.ChangeState(zombieInfo.zombieHitted);
            //objPooling.ReturnObject(gameObject);
            // 옵저버
        }
    }

}

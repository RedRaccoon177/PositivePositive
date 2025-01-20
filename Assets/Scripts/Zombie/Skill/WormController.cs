using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    public ZombieObjectPooling objPooling { get; set; }
    public ZombieController zombieInfo { get; set; }
    private void Start()
    {
        zombieInfo = GetComponentInParent<ZombieController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 로프
        if (collision.gameObject.tag == "Wall")
        {
            zombieInfo.DecreaseHp();
            //objPooling.ReturnObject(gameObject);
            // 옵저버
            zombieInfo.ChangeState(zombieInfo.zombieHitted);
        }
    }

}

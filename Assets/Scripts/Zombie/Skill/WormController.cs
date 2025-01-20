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
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log(" 벽 닿음");
            zombieInfo.ChangeState(zombieInfo.idle);
        }
    }

}

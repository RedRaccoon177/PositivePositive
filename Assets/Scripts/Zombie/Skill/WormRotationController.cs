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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("aas");
        if (collision.gameObject.tag == "RopeLastPoint")
        {
            Debug.Log("안녕ㅁㅁ");
            gameObject.SetActive(false);
            zombieInfo.ChangeState(zombieInfo.zombieHitted);
        }
    }

}

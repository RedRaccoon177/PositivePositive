using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormRotationController : MonoBehaviour
{
    public ZombieObjectPooling objPooling { get; set; }
    public ZombieController zombieInfo { get; set; }
    bool test = false;
    //public GameObject zombieii;
    private void Start()
    {
        zombieInfo  = transform.GetComponentInParent<ZombieController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RopeLastPoint")
        {
            test = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.GetComponent<Player>().GetAttackMode());
        if (collision.gameObject.tag == "Player" && test ==  true)
        {
            gameObject.SetActive(false);
            zombieInfo.ChangeState(zombieInfo.zombieHitted);
            test = false;
        }
    }

}

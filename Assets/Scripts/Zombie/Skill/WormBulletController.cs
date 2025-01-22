using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBulletController : MonoBehaviour
{
    Rigidbody2D wormBulletRigid;
    //public GameObject zombieInfo { get; set; }
    public GameObject playerInfo { get; set; }
    public ZombieWormBulletPool zombieBulletPool;
    float time;
    // 유도탄인지 구분
    Vector2 tempVector;

    void Update()
    {
        time += Time.deltaTime;
        wormBulletRigid = gameObject.GetComponent<Rigidbody2D>();
        tempVector = playerInfo.transform.position - transform.position;
        tempVector.Normalize();
        wormBulletRigid.velocity = tempVector * 4;
        if (time > 10)
        {
            zombieBulletPool.ReturnObject(gameObject);
        }
    }
    private void OnEnable()
    {
        time = 0;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        zombieBulletPool.ReturnObject(gameObject);
    //    }
    //}//
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            zombieBulletPool.ReturnObject(gameObject);
        }
    }
}

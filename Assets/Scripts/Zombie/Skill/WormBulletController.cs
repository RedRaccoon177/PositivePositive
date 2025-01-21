using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBulletController : MonoBehaviour
{
    Rigidbody2D wormBulletRigid;
    //public GameObject zombieInfo { get; set; }
    public GameObject playerInfo { get; set; }
    public ZombieWormBulletPool zombieBulletPool;
    // 유도탄인지 구분
    Vector2 tempVector;

    void Update()
    {
        wormBulletRigid = gameObject.GetComponent<Rigidbody2D>();
        tempVector = new Vector2(playerInfo.transform.position.x, playerInfo.transform.position.y);
        tempVector.Normalize();
        wormBulletRigid.velocity = tempVector * 15;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            zombieBulletPool.ReturnObject(gameObject);
        }
    }
}

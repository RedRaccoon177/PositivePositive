using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBulletController : MonoBehaviour
{
    Rigidbody2D wormBulletRigid;
    // 유도탄인지 구분
    int randBullet;
    int randX;
    int randY;

    void Start()
    {
        wormBulletRigid = gameObject.AddComponent<Rigidbody2D>();
        randBullet = Random.Range(0, 2);
        //randX =
        //randY =
    }

    void Update()
    {
        if (randBullet == 0)
        {
            wormBulletRigid.velocity = new Vector2();
        }
        else if (randBullet == 1)
        {
            wormBulletRigid.velocity = new Vector2();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
        }
    }
}

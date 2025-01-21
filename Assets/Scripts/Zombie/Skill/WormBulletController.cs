using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBulletController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            // 오브젝트 풀에서 비활성화
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerHealth = 100f; // 플레이어의 체력

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;  // 데미지를 받으면 체력 감소
        if (playerHealth <= 0)
        {
            playerHealth = 0;   // 체력이 0 이하가 되면
            Destroy(gameObject);// 플레이어 오브젝트 파괴
        }
    }
    void Start()
    { }

    void Update()
    { }
}

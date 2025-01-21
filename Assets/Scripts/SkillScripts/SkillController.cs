using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillController : MonoBehaviour
{
    public float speedPower = -10; // 스킬 속도
    public float damage = 10f;    // 스킬의 데미지

    void Start(){}

    void Update()
    {   
        transform.Translate(0.08f, 0, 0); // 스킬 이동

        if (transform.position.x < -10)
        {
            Destroy(gameObject); // 스킬이 화면 밖으로 나가면 옵젝 파괴
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            //Destroy(gameObject);
        }
    }
}
        

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillController : MonoBehaviour
{
    public float speedPower = -10; // ��ų �ӵ�
    public float damage = 10f;    // ��ų�� ������

    void Start(){}

    void Update()
    {   
        transform.Translate(0.08f, 0, 0); // ��ų �̵�

        if (transform.position.x < -10)
        {
            Destroy(gameObject); // ��ų�� ȭ�� ������ ������ ���� �ı�
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
        

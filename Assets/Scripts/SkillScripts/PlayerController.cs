using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerHealth = 100f; // �÷��̾��� ü��

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;  // �������� ������ ü�� ����
        if (playerHealth <= 0)
        {
            playerHealth = 0;   // ü���� 0 ���ϰ� �Ǹ�
            Destroy(gameObject);// �÷��̾� ������Ʈ �ı�
        }
    }
    void Start()
    { }

    void Update()
    { }
}

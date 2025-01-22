using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideareaskillScripts : MonoBehaviour
{
    public float speed = 20f; // ��ü �������� �ӵ�
    public float duration = 50f; // ��ų�� �ӹ����� �ð�
    public float stopYPosition = -0.22f; // ���ߴ� ��ġ

    void Start()
    {
        Destroy(gameObject, duration); // ������ �ð� �� ������Ʈ �ı�
    }
    void Update()
    {
        if (transform.position.y > stopYPosition)
        {
            //�� ��ü�� �������� �ӵ� ����
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            //�� ���ߴ� ��ġ ����
            transform.position = new Vector3(transform.position.x, stopYPosition, transform.position.z);
        }
    }
}

















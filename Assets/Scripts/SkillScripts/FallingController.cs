using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
    //�� ���� ������ �������� ��ų ������ �����ϴ� ����
    private static int currentFallingSkills = 0;
    //�� �ִ� ���� ������ �������� ��ų ����
    public static int maxFallingSkills = 10;
    //�� �ּ� ���� ������ �������� ��ų ����
    public static int minFallingSkills = 5;
    //�� x,y.z ���� tempPosition ����
    Vector3 tempPosition; 
    //�� �������� �ӵ� ����
    float moveSpeed;
    float startLine;

    void Start()
    {
        //�� ���� ��ų ������ �ִ� ������ ���� �ʾ��� ��쿡�� ����
        if (currentFallingSkills < maxFallingSkills)
        {
            //�� �������� ��ų ���� ����
            currentFallingSkills++;

            //�� transform.position�� tempPosition�� ���
            tempPosition = transform.position;
            //�� �������� �ӵ��� ���������� ����
            moveSpeed = Random.Range(0.5f, 0.7f);
            //�� ���� ���� ���������� ����
            startLine = Random.Range(22f, 23f);
            tempPosition.y = startLine;
            //�� x��ǥ ���������� ����
            tempPosition.x = Random.Range(-8.2f, 2);
            //�� ��ġ ������Ʈ
            transform.position = tempPosition;
        }
        else
        {
            // �ִ� ������ �ʰ��ϸ� ������Ʈ�� �ı�
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�� �浹�� ��ü�� ���� ���
        if (collision.gameObject.tag == "Wall")  
        {
            //�� �������� ��ų ���� ����
            currentFallingSkills--;
            //�� ��ü �ı�
            Destroy(gameObject);
            
        }
    }
    private void FixedUpdate()
    {
        //�� moveSpeed�� -1 �� tempPosition.y�� �ݺ��ؼ� �ֱ�
        tempPosition.y -= moveSpeed;

        //�� y�� -6���� �۾�����
        if (tempPosition.y < -9) 
        {
            //�� y�� +30 ���ϱ�
            tempPosition.y += 30;
            //�� x��ǥ�� ���������� �����Ͽ� �ݺ�
            tempPosition.x = Random.Range(-7.2f, 1.98f); 
            //�� moveSpeed�� ���������� �����ؼ� �ݺ�
            moveSpeed = Random.Range(0.5f, 0.7f); 
        }
        //�� tempPosition�� transform.position�� ����ֱ�
        transform.position = tempPosition;
    }
    
    void Update()
    {
       
    }
}

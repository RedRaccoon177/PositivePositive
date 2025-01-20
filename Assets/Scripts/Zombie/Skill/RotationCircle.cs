using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RotationCircle : MonoBehaviour
{
    
    public GameObject centerRotation { get; set; }
    //public ZombieObjectPooling objectPool;
    // 반지름
    public float radius { get; set; }
    // 원 스피드
    public float rotationSpeed { get; set; }
    // 각도
    float angle;
    // Worm과 Worm 사이의 각도
    public float angleX;
    public float angleY;
    public int num;
    void Update()
    {
        angle += Time.deltaTime * rotationSpeed;
        //angle %= 360;
        float ran = angle * Mathf.Deg2Rad;
        angleX = centerRotation.transform.position.x + (radius * Mathf.Cos(ran));
        angleY = centerRotation.transform.position.y + (radius * Mathf.Sin(ran));
        transform.position = new Vector3(angleX, angleY, 0);
    }

    public void CreatWormShield(int i)
    {
        angle = 360 - (90 *i);
        float ran = angle * Mathf.Deg2Rad;
        angleX = centerRotation.transform.position.x + (radius * Mathf.Cos(ran));
        angleY = centerRotation.transform.position.y + (radius * Mathf.Sin(ran));
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // 플레이어 로프와 닿았을때로 바꿔야 함
    //    if (collision.gameObject.tag == "Wall")
    //    {
    //        OnWormDestroy(gameObject);
    //    }
    //}

    //public void OnWormDestroy(GameObject wormShield)
    //{
    //    // 오브젝트 풀로 반환
    //     objectPool.ReturnObject(wormShield);
    //}

}
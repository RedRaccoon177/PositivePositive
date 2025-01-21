using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideareaskillScripts : MonoBehaviour
{
    public float speed = 20f; // 구체 내려오는 속도
    public float duration = 50f; // 스킬이 머무르는 시간
    public float stopYPosition = -0.22f; // 멈추는 위치

    void Start()
    {
        Destroy(gameObject, duration); // 지정된 시간 후 오브젝트 파괴
    }
    void Update()
    {
        if (transform.position.y > stopYPosition)
        {
            //▼ 구체가 내려오는 속도 설정
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            //▼ 멈추는 위치 설정
            transform.position = new Vector3(transform.position.x, stopYPosition, transform.position.z);
        }
    }
}

















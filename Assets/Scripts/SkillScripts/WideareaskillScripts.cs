using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideareaskillScripts : MonoBehaviour
{
    public float speed = 20f; // 구체 내려오는 속도
    public float duration = 50f; // 스킬이 머무르는 시간
    public float stopYPosition = -0.22f; // 멈추는 위치
    public float explosionDelay = 5f; // 구체가 터지기 전 대기 시간

    public float explosionTimer; // 터지기 전 타이머

    private bool isActivated = false; // 구체 스킬의 초기 활성화 상태


    void Start()
    {
        Destroy(gameObject, duration); // 지정된 시간 후 오브젝트 파괴
        explosionTimer = explosionDelay;
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

            //▼ 구체가 터지기 전 타이머 감소
            explosionTimer -= Time.deltaTime;

            //▼ 타이머가 0 이하가 되면 구체 파괴
            if(explosionTimer <= 0f)
            {
                Destroy(gameObject); // 구체 오브젝트 파괴
            }
        }
    }

    public void ActivateWideAreaSkill()
    {
        isActivated = true;
    }
}

















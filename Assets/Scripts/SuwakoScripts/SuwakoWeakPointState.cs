using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuwakoWeakPointState : SuwakoState
{


    //타격 받을 경우 색 변환 => 점점 어두워짐.

    //4대 이상 얻어 맞으면 다시 원래 상태로 돌아옴.

    public float maxHealth; // 최대 체력
    private float damage;   // 데미지 받았을 때

    float usingTime = 10f; // 전환 시간 (초)
    float endTime = 0;

    int hitCount = 0; 

    public override void Enter(SuwakoController suwako)
    {
        maxHealth = 20;
        damage = 1;
        hitCount = 0;
        endTime = 0;

        //박스콜라이더 온 오프, 물리 방어, 파티클 실행
        suwako.boxCollider.enabled = false;
        suwako.weakPointCollider.enabled = true;
        suwako.rb.bodyType = RigidbodyType2D.Kinematic;
        suwako.childObjects[8].SetActive(true);
        suwako.animator.SetBool("IsIdle", false);
        suwako.animator.SetBool("IsWeakPoint", true);

        endTime = Time.time + usingTime;
    }

    public override void Update(SuwakoController suwako)
    {
        if(Time.time > endTime)
        {
            suwako.animator.SetBool("IsWeakPoint", false);
            suwako.boxCollider.enabled = true;
            suwako.weakPointCollider.enabled = false;
            suwako.rb.bodyType = RigidbodyType2D.Dynamic;
            suwako.childObjects[8].SetActive(false);
            suwako.ChangeState(suwako.idleState);
        }
    }

    public override void OnCollisionEnter2D(SuwakoController suwako, Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (hitCount < 4)
            {
                TakeDamage(suwako);
            }
            else if (hitCount >= 4)
            {
                suwako.animator.SetBool("IsWeakPoint", false);
                suwako.boxCollider.enabled = true;
                suwako.weakPointCollider.enabled = false;
                suwako.rb.bodyType = RigidbodyType2D.Dynamic;
                suwako.childObjects[8].SetActive(false);
                suwako.ChangeState(suwako.idleState);
            }
        }
    }

    public void TakeDamage(SuwakoController suwako)
    {
        hitCount++;
        suwako.suwakoHP -= damage;
        suwako.suwakoHP = Mathf.Clamp(suwako.suwakoHP, 0, maxHealth); // 체력을 0~최대값 사이로 제한
        //테스트용 승리
        if (suwako.suwakoHP <= 0)
        {
            suwako.suwakoHP = 0;
            GameManager.Instance.Victory();
        }
        suwako.GetComponent<MonsterHPObserver>().NotifyHealthChange(maxHealth, suwako.suwakoHP);
    }
}

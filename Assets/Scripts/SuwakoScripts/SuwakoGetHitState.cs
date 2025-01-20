using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoGetHitState : SuwakoState
{
    public float maxHealth; // 최대 체력
    private float damage;   // 데미지 받았을 때
    public float monsterHPfillAmount;

    // 체력 변화 알림을 위한 이벤트
    public event Action<float> OnHealthChanged;

    //하이라이키창 ui의 스크립트를 가져와서, enter 활성화 하기
    MonsterHP monsterHP;

    public override void Enter(SuwakoController suwako)
    {
        //스와코 데미지 받는 애니메이션 삽입.
        maxHealth = 20;
        damage = 1;
        monsterHPfillAmount = suwako.suwakoHP / maxHealth;
    }

    public void TakeDamage(SuwakoController suwako)
    {
        suwako.suwakoHP -= damage;
        suwako.suwakoHP = Mathf.Clamp(suwako.suwakoHP, 0, maxHealth); // 체력을 0~최대값 사이로 제한
        NotifyHealthChanged(suwako);
    }

    public override void Update(SuwakoController suwako)
    {
        monsterHPfillAmount = suwako.suwakoHP / maxHealth;

        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바로 체력 감소
        {
            Debug.Log("스페이스바");
            TakeDamage(suwako);
        }
    }

    // 옵저버들에게 체력 변화를 알림
    private void NotifyHealthChanged(SuwakoController suwako)
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(monsterHPfillAmount); // 체력 비율을 전달
        }
    }
}

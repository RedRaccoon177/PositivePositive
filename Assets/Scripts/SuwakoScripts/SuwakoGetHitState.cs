using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoGetHitState : SuwakoState
{
    //public float maxHealth; // 최대 체력
    //private float damage;   // 데미지 받았을 때
    //public float monsterHPfillAmount;

    //public override void Enter(SuwakoController suwako)
    //{
    //    //스와코 데미지 받는 애니메이션 삽입.
    //    maxHealth = 20;
    //    damage = 1;
    //    monsterHPfillAmount = suwako.suwakoHP / maxHealth;
    //}


    //public override void Update(SuwakoController suwako)
    //{
    //    monsterHPfillAmount = suwako.suwakoHP / maxHealth;

    //    if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바로 체력 감소
    //    {
    //        Debug.Log("스페이스바");
    //        TakeDamage(suwako);
    //    }
    //}

    //public void TakeDamage(SuwakoController suwako)
    //{
    //    suwako.suwakoHP -= damage;
    //    suwako.suwakoHP = Mathf.Clamp(suwako.suwakoHP, 0, maxHealth); // 체력을 0~최대값 사이로 제한
    //    //테스트용 승리
    //    if (suwako.suwakoHP <= 0)
    //    {
    //        suwako.suwakoHP = 0;
    //        GameManager.Instance.Victory();
    //    }
    //    //NotifyHealthChanged(suwako);
    //    suwako.GetComponent<MonsterHPObserver>().NotifyHealthChange(maxHealth, suwako.suwakoHP);
    //}
}

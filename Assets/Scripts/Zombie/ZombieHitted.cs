using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieHitted : ZombieState
{
    public float maxHealth; // 최대 체력
    private float damage;   // 데미지 받았을 때
    public float monsterHPfillAmount;

    // 체력 변화 알림을 위한 이벤트
    public event Action<float> OnHealthChanged;

    MonsterHP monsterHP;

    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsHittedd",true);
        maxHealth = 20;
        damage = 5;
        TakeDamage(zombie);
    }

    public void TakeDamage(ZombieController zombie)
    {
        zombie.zombieHp -= damage;
        zombie.zombieHp = Mathf.Clamp(zombie.zombieHp, 0, maxHealth); // 체력을 0~최대값 사이로 제한
        monsterHPfillAmount = zombie.zombieHp / maxHealth;
        NotifyHealthChanged(zombie);
    }

    public override void Update(ZombieController zombie)
    {
        if (zombie.isHitted == true)
        {
            zombie.Animator.SetBool("IsHittedd",false);
        }
    }

    // 옵저버들에게 체력 변화를 알림
    private void NotifyHealthChanged(ZombieController zombie)
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(monsterHPfillAmount); // 체력 비율을 전달
        }
    }

}
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


    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsHittedd",true);
        maxHealth = 20;
        damage = 1;
        TakeDamage(zombie);
    }
    public void TakeDamage(ZombieController zombie)
    {
        zombie.zombieHp -= damage;
        zombie.zombieHp = Mathf.Clamp(zombie.zombieHp, 0, maxHealth); // 체력을 0~최대값 사이로 제한
        if(zombie.zombieHp == 0)
        {
            zombie.ChangeState(zombie.zombiDie);
        }
        zombie.GetComponent<MonsterHPObserver>().NotifyHealthChange(maxHealth,zombie.zombieHp);
    }
    public override void Update(ZombieController zombie)
    {
        if (zombie.isHitted == true)
        {
            zombie.Animator.SetBool("IsHittedd",false);
            zombie.ChangeState(zombie.idle);
        }
    }

}
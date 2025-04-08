using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ZombieWormBullet : ZombieState
{
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsSkillWormBullet" , true);
    }

    public override void FixUpdate(ZombieController zombie)
    {
    }
    public override void Update(ZombieController zombie)
    {
        zombie.deltaTime += Time.deltaTime;
        if (zombie.deltaTime > 5)
        {
            zombie.Animator.SetBool("IsSkillWormBullet" , false);
        }

        // 오브젝트 풀링에서 활성화 벌레 총알
    }
}

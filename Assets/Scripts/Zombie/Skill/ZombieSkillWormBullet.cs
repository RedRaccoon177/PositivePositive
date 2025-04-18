using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSkillWormBullet : ZombieState
{
    float changeTime;
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsHittedd", false);
        zombie.Animator.SetBool("IsSkillWormBullet",true);
    }
    public override void FixUpdate(ZombieController zombie)
    {
        zombie.mosterToPlayer = zombie.PlayerInfo.transform.position - zombie.transform.position;
        zombie.transform.rotation = Quaternion.Euler(0, 0, 180 + Mathf.Atan2(zombie.mosterToPlayer.y, zombie.mosterToPlayer.x) * Mathf.Rad2Deg);
    }
    public override void Update(ZombieController zombie)
    {
        zombie.deltaTime += Time.deltaTime;
        changeTime += Time.deltaTime;
        if (zombie.deltaTime > 1)
        {
            zombie.deltaTime = 0;
            zombie.zomBulletObjPool.WormBulletActiveTrue(zombie);
        }
        if (changeTime >2)
        {
            changeTime = 0;
            zombie.Animator.SetBool("IsSkillWormBullet", false);
            zombie.ChangeState(zombie.idle);
        }
    }

}

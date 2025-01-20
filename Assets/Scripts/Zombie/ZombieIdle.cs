using System.Collections;
using System.ComponentModel;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieIdle : ZombieState
{
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsWalk", false);
        zombie.Animator.SetBool("IsJump", false);
        zombie.Animator.SetBool("SkillBlack", false);
        zombie.Rigid.velocity = Vector3.zero;
        zombie.directionX = 0;
        zombie.directionY = 0;
        zombie.randState = UnityEngine.Random.Range(1,1);
        zombie.zomObjPool.AllActiveTrue();
    }

    public override void Update(ZombieController zombie)
    {
        zombie.deltaTime += Time.deltaTime;
        if (zombie.deltaTime > 3)
        {
            zombie.deltaTime = 0;
            zombie.zomObjPool.AllActiveFalse(zombie.WormPrepeb);
            Debug.Log("시간 조건문");
            zombie.deltaTime = 0;
            if (zombie.randState == 0)
            {
                zombie.ChangeState(zombie.walk);
            }
            else if (zombie.randState == 1)
            {
                zombie.ChangeState(zombie.jumpReady);
                //Debug.Log("스킬상태");
            }
            else if (zombie.randState == 2)
            {
                zombie.ChangeState(zombie.skillBlackHole);
            }
        }
    }
    //public IEnumerator IdleWait(ZombieController zombie)
    //{
    //    zombie.zomObjPool.AllActiveTrue();
    //    yield return new WaitForSeconds(2f);
    //    if (zombie.randState == 0)
    //    {
    //        zombie.ChangeState(zombie.walk);
    //    }
    //    else if (zombie.randState == 1)
    //    {
    //        zombie.ChangeState(zombie.jumpReady);
    //        //Debug.Log("스킬상태");
    //    }
    //    else if (zombie.randState == 2)
    //    {
    //        zombie.ChangeState(zombie.skillBlackHole);
    //    }
    //}

}
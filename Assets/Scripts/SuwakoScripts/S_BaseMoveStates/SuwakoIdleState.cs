using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuwakoIdleState : SuwakoState
{
    float idleTime = 0;

    bool test = false;

    public override void Enter(SuwakoController suwako)
    {
        suwako.animator.SetBool("IsIdle", true);
        suwako.animator.SetInteger("IsFalling", 0);
        suwako.animator.SetInteger("IsFlying", 0);
        suwako.animator.SetBool("IsJump", false);
        suwako.animator.SetBool("IsLanding", false);
        suwako.animator.SetInteger("IsSkills", 0);
        suwako.animator.SetInteger("IsRiverSkill", 0);
        suwako.animator.SetBool("IsWeakPoint", false);

        //idleTime 대기 시간 초로 지정하기
        idleTime = Time.time + 2;

        suwako.moveORskillORweak = 999;

    }
    public override void Update(SuwakoController suwako)
    {
        if (Time.time > idleTime)
        {
            test = true;
        }

        if (test == true)
        {
            test = false;
            suwako.ChangeState(suwako.skill0_ShootingBullet);
        }
        ////강제로 idle 상태 idleTime만큼 대기
        //if (Time.time > idleTime)
        //{
        //    if (suwako.stateCount < 3)
        //    {
        //        //기본 상태
        //        suwako.moveORskillORweak = 0;
        //    }
        //    else if (suwako.stateCount == 3)
        //    {
        //        suwako.moveORskillORweak = 1;
        //    }
        //    else if (suwako.stateCount > 3)
        //    {
        //        suwako.moveORskillORweak = 2;
        //    }
        //}

        ////기본 상태 진입
        //if (suwako.moveORskillORweak == 0)
        //{
        //    suwako.whatBaseState = Random.Range(0, 5);
        //    suwako.stateCount++;

        //    if (0 <= suwako.whatBaseState && suwako.whatBaseState <= 1)
        //    {
        //        //좌우로 이동하는 상태
        //        suwako.ChangeState(suwako.walkFrontState);
        //    }
        //    else if (suwako.whatBaseState == 2)
        //    {
        //        //날아 오르는 상태
        //        suwako.ChangeState(suwako.flyingState);
        //    }
        //    else if (suwako.whatBaseState == 3)
        //    {
        //        //점프한 상태
        //        suwako.ChangeState(suwako.jumpingState);
        //    }
        //}
        //else if(suwako.moveORskillORweak == 1)
        //{
        //    suwako.whatBaseState = Random.Range(0, 2);
        //    suwako.stateCount++;

        //    if (suwako.whatBaseState == 0)
        //    {
        //        suwako.ChangeState(suwako.skill0_ShootingBullet);
        //    }
        //    else if (suwako.whatBaseState == 1)
        //    {
        //        suwako.ChangeState(suwako.skill2_RiverFlowing);
        //    }
        //}
        //else if(suwako.moveORskillORweak == 2)
        //{
        //    suwako.stateCount = 0;
        //    suwako.ChangeState(suwako.weakPointState);
        //}

        
    }

    public override void OnCollisionEnter2D(SuwakoController suwako, Collision2D collision)
    {
        //땅에 닿으면 마찰력을 0으로 만들 장치
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            suwako.rb.velocity = new Vector2();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuwakoIdleState : SuwakoState
{
    bool isIdle = true;
    float idleTime = 0;
    int whatState = 0;

    public override void Enter(SuwakoController suwako)
    {
        suwako.animator.SetBool("IsIdle", true);
        suwako.animator.SetInteger("IsFalling", 0);
        suwako.animator.SetInteger("IsFlying", 0);
        suwako.animator.SetBool("IsJump", false);
        suwako.animator.SetBool("IsLanding", false);
        suwako.animator.SetInteger("IsSkills", 0);
        suwako.animator.SetInteger("IsRiverSkill", 0);


        //idle 상태 3초간 유지를 위한 식
        idleTime = Time.time + 3;
        whatState = 0;

        //false로 다시 바꿔야함.
        isIdle = false;
    }
    public override void Update(SuwakoController suwako)
    {
        if (isIdle == true)
        {
            //상태를 랜덤값으로 돌려라
            whatState = Random.Range(8, 9);
        }

        if (whatState == 0)
        {
            if (Time.time >= idleTime)
            {
                isIdle = true;
            }
        }
        else if (1 <= whatState && whatState <= 4)
        {
            //좌우로 이동하는 상태
            suwako.ChangeState(suwako.walkFrontState);
        }
        else if (whatState == 5)
        {
            //날아 오르는 상태
            suwako.ChangeState(suwako.flyingState);
        }
        else if (whatState == 6)
        {
            //점프한 상태
            suwako.ChangeState(suwako.jumpingState);
        }
        else if(whatState == 7)
        {
            suwako.ChangeState(suwako.skill0_ShootingBullet);
        }
        else if (whatState == 8)
        {
            suwako.ChangeState(suwako.skill2_RiverFlowing);
        }
        //임시로 만든 타격 시임.
        else if( whatState == 99)
        {
            suwako.ChangeState(suwako.GetHitState);
        } 


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

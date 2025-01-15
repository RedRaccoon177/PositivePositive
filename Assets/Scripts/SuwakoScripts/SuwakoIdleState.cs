using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    bool isIdle = true;
    float idleTime = 0;

    //추후 삭제 해도 됨.
    int whatState = 0;

    public override void Enter(SuwakoController suwako)
    {
        suwako.animator.SetBool("IsIdle", true);

        //idle 상태 3초간 유지를 위한 식
        idleTime = Time.time + 3;
        whatState = 0;
        isIdle = false;
    }
    public override void Update(SuwakoController suwako)
    {
        if (isIdle == true)
        {
            //상태를 랜덤값으로 돌려라
            whatState = Random.Range(0, 4);
        }

        if (whatState == 0)
        {
            if (Time.time >= idleTime)
            {
                isIdle = true;
            }
        }
        else if (whatState == 1)
        {
            //좌우로 이동하는 상태
            suwako.ChangeState(suwako.walkFrontState);
        }
        else if (whatState == 2)
        {
            //날아 오르는 상태
            suwako.ChangeState(suwako.flyingState);
        }
        else if (whatState == 3)
        {
            //점프한 상태
            suwako.ChangeState(suwako.jumpingState);
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

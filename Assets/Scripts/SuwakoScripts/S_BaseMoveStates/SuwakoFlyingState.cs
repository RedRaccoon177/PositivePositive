﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoFlyingState : SuwakoState
{
    int flyingState = 0;
    float flyingTime = 0;
    bool isCorret = false;

    //skill1 (0이면 스킬 사용x, 1이면 스킬 사용)
    int isSkill1 = 0;

    public override void Enter(SuwakoController suwako)
    {
        isSkill1 = Random.Range(0, 2);
        suwako.RiORLe = Random.Range(0, 2) == 0 ? -1 : 1;
        suwako.animator.SetInteger("IsFlying", 1);
        flyingTime = Time.time + 5;
        flyingState = 0;
        isCorret = true;
    }
    public override void Update(SuwakoController suwako)
    {
        if (Time.time < flyingTime)
        {
            isCorret = true;
        }
        else
        {
            isCorret = false;
        }

        //날고 있는 중이면
        if (flyingState == 0)
        {
            suwako.animator.SetInteger("IsFlying", 2);
        }
        //나는 것이 끝나면
        else if (flyingState == 1)
        {
            if (isSkill1 == 0)
            {
                suwako.animator.SetInteger("IsFlying", 3);
                suwako.ChangeState(suwako.fallingState);
            }
            else if(isSkill1 == 1)
            {
                suwako.ChangeState(suwako.skill1_JumpORFlyShootingBullet);
            }
        }
    }
    public override void FixUpdate(SuwakoController suwako)
    {
        //5초동안 실행 되다가 5초가 지나면 스탑
        if (isCorret == true)
        {
            flyingState = 0;

            if (suwako.RiORLe == -1)
            {
                suwako.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (suwako.RiORLe == 1)
            {
                suwako.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            suwako.rb.velocity = new Vector2(suwako.RiORLe, suwako.flyingPower);
        }
        else
        {
            flyingState = 1;
        }
    }
}

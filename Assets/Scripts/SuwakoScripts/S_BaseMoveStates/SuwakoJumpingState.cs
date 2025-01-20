using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoJumpingState : SuwakoState
{
    bool isJump = true;
    public float bounceMultiplier = 1.0f; // 튕김 배율

    //skill1 (0이면 스킬 사용x, 1이면 스킬 사용)
    int isSkill1 = 0;

    public override void Enter(SuwakoController suwako)
    {
        isJump = true;
        isSkill1 = Random.Range(0, 2);

        suwako.RiORLe = Random.Range(0, 3);
        if(suwako.RiORLe == 2)
        {
            suwako.RiORLe = -1;
        }
        suwako.animator.SetBool("IsJump", true);
        if (suwako.RiORLe == -1)
        {
            suwako.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (suwako.RiORLe == 1)
        {
            suwako.transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        //점프 실행
        if (isJump == true)
        {
            suwako.rb.AddForce(new Vector2(5 * suwako.RiORLe, suwako.jumpPower), ForceMode2D.Impulse);
            isJump = false;
        }
    }
    public override void Update(SuwakoController suwako)
    {
        //떨어지기 시작
        if (suwako.rb.velocity.y < 0)
        {
            suwako.animator.SetBool("IsJump", false);
            if (isSkill1 == 0)
            {
                suwako.ChangeState(suwako.fallingState);
            }
            else if (isSkill1 == 1)
            {
                suwako.ChangeState(suwako.skill1_JumpORFlyShootingBullet);
            }
        }
    }
}
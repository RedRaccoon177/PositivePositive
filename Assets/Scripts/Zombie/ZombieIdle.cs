using UnityEngine;

public class ZombieIdle : ZombieState
{
    float stateTime;
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsWalk", false);
        zombie.Animator.SetBool("IsHittedd", false);
        zombie.Animator.SetBool("IsJump", false);
        zombie.Animator.SetBool("IsJumpReady", false);
        zombie.Animator.SetBool("SkillBlack", false);
        zombie.Rigid.velocity = Vector3.zero;
        zombie.directionX = 0;
        zombie.directionY = 0;
        zombie.randState = Random.Range(0,13);
        zombie.zomObjPool.AllActiveTrue();
    }

    public override void Update(ZombieController zombie)
    {
        stateTime += Time.deltaTime;
        if (stateTime > 2)
        {
            stateTime = 0;
            zombie.zomObjPool.AllActiveFalse();
            if (zombie.isWalked == false)
            {
                zombie.isWalked = true;
                zombie.ChangeState(zombie.walk);
            }
            else if(zombie.isWalked == true)
            {
                zombie.isWalked = false;
                if (0 <= zombie.randState  && zombie.randState <= 6)
                {
                    //zombie.Animator.SetBool("JumpReady", true);
                    zombie.ChangeState(zombie.jumpReady);
                }
                else if (6 < zombie.randState && zombie.randState <= 8)
                {
                    zombie.ChangeState(zombie.skillBlackHole);
                }
                else if (8 < zombie.randState && zombie.randState < 13)
                {
                    zombie.ChangeState(zombie.skillWormBullet);
                }
            }
        }
    }
}
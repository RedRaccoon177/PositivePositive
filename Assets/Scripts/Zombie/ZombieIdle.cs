using UnityEngine;

public class ZombieIdle : ZombieState
{
    float stateTime;
    public override void Enter(ZombieController zombie)
    {
        //zombie.Animator.SetBool("IsWalk", false);
       // zombie.Animator.SetBool("IsJump", false);
        //zombie.Animator.SetBool("SkillBlack", false);
        zombie.Rigid.velocity = Vector3.zero;
        zombie.directionX = 0;
        zombie.directionY = 0;
        zombie.randState = 1;//UnityEngine.Random.Range(0,4);
        zombie.zomObjPool.AllActiveTrue();
    }

    public override void Update(ZombieController zombie)
    {
        stateTime += Time.deltaTime;
        if (stateTime > 4)
        {
            stateTime = 0;
            zombie.zomObjPool.AllActiveFalse();
            if (zombie.randState == 0)
            {
                zombie.ChangeState(zombie.walk);
            }
            else if (zombie.randState == 1)
            {
                //zombie.Animator.SetBool("JumpReady", true);
                zombie.ChangeState(zombie.jumpReady);
            }
            else if (zombie.randState == 2)
            {
                zombie.ChangeState(zombie.skillBlackHole);
            }
            else if (zombie.randState == 3)
            {
                zombie.ChangeState(zombie.skillWormBullet);
            }
        }
    }
}
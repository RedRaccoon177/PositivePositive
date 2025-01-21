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
        zombie.randState = 3;//UnityEngine.Random.Range(0,3);
        zombie.zomObjPool.AllActiveTrue();
    }

    public override void Update(ZombieController zombie)
    {
        zombie.deltaTime += Time.deltaTime;
        if (zombie.deltaTime > 3)
        {
            zombie.zomObjPool.AllActiveFalse();
            zombie.deltaTime = 0;
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
            else if (zombie.randState == 3)
            {
                zombie.ChangeState(zombie.skillWormBullet);
            }
        }
    }
}
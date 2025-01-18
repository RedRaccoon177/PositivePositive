using System.Collections;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieIdle : ZombieState
{
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsWalk", false);
        zombie.Rigid.velocity = Vector3.zero;
        zombie.directionX = 0;
        zombie.directionY = 0;
        zombie.randState = UnityEngine.Random.Range(0, 1);
        //Debug.Log(zombie.randState);
        zombie.CorutinPlay(IdleWait(zombie));
        //zombie.StartCoroutine(IdleWait(zombie));
    }
    public IEnumerator IdleWait(ZombieController zombie)
    {
        zombie.zomObjPool.AllActiveTrue();
        yield return new WaitForSeconds(2f);
        if (zombie.randState == 0)
        {
            zombie.ChangeState(zombie.walk);
            //Debug.Log("걷는상태");
        }
        else if (zombie.randState == 1)
        {
            zombie.ChangeState(zombie.jumpReady);
            //Debug.Log("스킬상태");
        }
    }

}
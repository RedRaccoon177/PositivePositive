using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDie : ZombieState
{
    GameObject gameOver;
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsDie",true);
    }
    public override void Update(ZombieController zombie)
    {
        if (zombie.isDie == true)
        {
            GameManager.Instance.Victory();
        }
    }
}

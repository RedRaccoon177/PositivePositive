using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDie : ZombieState
{
    GameObject gameOver;
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsDie",true);
        // 게임 승리
    }
}

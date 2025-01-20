using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieHitted : ZombieState
{
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetTrigger("IsHitted");
    }
}

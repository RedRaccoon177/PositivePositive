using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class SuwakoFallingState : SuwakoState
{
    public override void Enter(SuwakoController suwako)
    {
        suwako.landing = false;
        suwako.falling = 1;
        suwako.animator.SetInteger("IsFalling", suwako.falling);
    }
    public override void Update(SuwakoController suwako)
    {
        if (suwako.landing == true)
        {
            suwako.ChangeState(suwako.landingState);
        }
    }
}

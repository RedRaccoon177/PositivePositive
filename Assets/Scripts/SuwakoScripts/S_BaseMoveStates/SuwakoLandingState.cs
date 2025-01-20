using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoLandingState : SuwakoState
{
    override public void Enter(SuwakoController suwako) 
    {
        suwako.animator.SetBool("IsLanding", true);
    }

    public override void Update(SuwakoController suwako)
    {
        if (suwako.landing == true)
        {
            suwako.ChangeState(suwako.idleState);
        }
    }
}

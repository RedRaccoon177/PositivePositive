using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoWalkFrontState : SuwakoState
{
    public override void Enter(SuwakoController suwako)
    {
        suwako.animator.SetTrigger("IsWalkFront");
        suwako.RiORLe = Random.Range(0, 2) == 0 ? -1 : 1;
        //우측에서 좌로
        if (suwako.RiORLe == -1)
        {
            suwako.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        //좌측에서 우로
        else if (suwako.RiORLe == 1)
        {
            suwako.transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        suwako.rb.AddForce(new Vector2(suwako.RiORLe * suwako.xORy, suwako.frontJumpPower), ForceMode2D.Impulse);
    }
    public override void Update(SuwakoController suwako)
    {
        if(suwako.rb.velocity.y < 0)
        {
            suwako.ChangeState(suwako.fallingState);
        }
    }
}

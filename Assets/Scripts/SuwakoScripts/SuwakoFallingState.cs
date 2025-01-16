using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class SuwakoFallingState : SuwakoState
{
    public override void Enter(SuwakoController suwako)
    {
        suwako.falling = 1;
        suwako.animator.SetInteger("IsFalling", suwako.falling);
    }
    public override void Update(SuwakoController suwako)
    {
        if (suwako.falling == 2)
        {
            suwako.animator.SetInteger("IsFalling", suwako.falling);
            suwako.ChangeState(suwako.idleState);
        }
    }
    public override void FixUpdate(SuwakoController suwako) { }
    public override void OnCollisionEnter2D(SuwakoController suwako, Collision2D collision) { }
    public override void OnTriggerEnter2D(SuwakoController suwako, Collider2D collider) { }
    public override void OnTriggerExit2D(SuwakoController suwako, Collider2D collider) { }
}

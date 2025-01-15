using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class FallingState : State
{
    int falling = 1;

    public override void Enter(SuwakoController suwako)
    {
        falling = 1;
        suwako.animator.SetInteger("IsFalling", falling);
    }
    public override void Update(SuwakoController suwako)
    {
        if (falling == 2)
        {
            suwako.animator.SetInteger("IsFalling", falling);
            suwako.ChangeState(suwako.idleState);
        }
    }
    public override void FixUpdate(SuwakoController suwako)
    {

    }
    public override void OnCollisionEnter2D(SuwakoController suwako, Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            falling = 2;
        }
    }
    public override void OnTriggerEnter2D(SuwakoController suwako, Collider2D collider)
    {
        if (collider.tag == "Wall")
        {
            suwako.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
    public override void OnTriggerExit2D(SuwakoController suwako, Collider2D collider)
    {
        if (collider.tag == "Wall")
        {
            suwako.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}

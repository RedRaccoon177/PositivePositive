using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoJumpingState : SuwakoState
{
    bool isJump = true;

    public float bounceMultiplier = 1.0f; // ∆®±Ë πË¿≤

    public override void Enter(SuwakoController suwako)
    {
        isJump = true;
        suwako.RiORLe = Random.Range(0, 3);
        if(suwako.RiORLe == 2)
        {
            suwako.RiORLe = -1;
        }
        suwako.animator.SetBool("IsJump", true);
        if (suwako.RiORLe == -1)
        {
            suwako.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (suwako.RiORLe == 1)
        {
            suwako.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        //¡°«¡ Ω««‡
        if (isJump == true)
        {
            suwako.rb.AddForce(new Vector2(5 * suwako.RiORLe, suwako.jumpPower), ForceMode2D.Impulse);
            isJump = false;
        }
    }

    public override void Update(SuwakoController suwako)
    {
        //∂≥æÓ¡ˆ±‚ Ω√¿€
        if (suwako.rb.velocity.y < 0)
        {
            suwako.animator.SetBool("IsJump", false);
            suwako.ChangeState(suwako.fallingState);
        }
    }

    public override void FixUpdate(SuwakoController suwako) { }
    public override void OnCollisionEnter2D(SuwakoController suwako, Collision2D collision) { }
    public override void OnTriggerEnter2D(SuwakoController suwako, Collider2D collider) { }
    public override void OnTriggerExit2D(SuwakoController suwako, Collider2D collider) { }
}
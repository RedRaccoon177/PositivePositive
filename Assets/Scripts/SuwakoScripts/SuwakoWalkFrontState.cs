using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkFrontState : State
{
    float changeSec = 2;
    int walkCount = 4;
    bool isWalkFront = false;
    float twoSec = 0;

    public override void Enter(SuwakoController suwako)
    {
        walkCount = 4;
        isWalkFront = false;
        twoSec = Time.time + changeSec;
    }
    public override void Update(SuwakoController suwako)
    {
        //2초마다 점프 하는게 자연스럽다.
        if (Time.time >= twoSec)
        {
            if (walkCount != 0)
            {
                twoSec = Time.time + changeSec;
                isWalkFront = true;
                suwako.animator.SetTrigger("IsWalkFront");
                walkCount--;
            }
            else if (walkCount <= 0)
            {
                suwako.ChangeState(new IdleState());
            }
        }
    }
    public override void FixUpdate(SuwakoController suwako)
    {
        if (isWalkFront == true)
        {
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

            isWalkFront = false;
        }
    }
    public override void OnCollisionEnter2D(SuwakoController suwako, Collision2D collision)
    {

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

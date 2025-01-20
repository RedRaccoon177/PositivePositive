using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSkillBlackHole : ZombieState
{
    RotationCircle blackHoleRotationScript;
    public override void Enter(ZombieController zombie)
    {
        blackHoleRotationScript = zombie.BlackHoleSkillPrepeb.transform.GetChild(0).GetComponent<RotationCircle>();
        zombie.Animator.SetBool("SkillBlack",true);
        int randX = Random.Range(-15,15);
        int randY = Random.Range(-15,15);

        zombie.BlackHoleSkillPrepeb.transform.position = new Vector2 (randX,randY);
        zombie.BlackHoleSkillPrepeb.SetActive(true);
        blackHoleRotationScript.centerRotation = zombie.BlackHoleSkillPrepeb;
        blackHoleRotationScript.rotationSpeed = 500f;
        blackHoleRotationScript.radius = 1f;
    }

    public override void FixUpdate(ZombieController zombie)
    {
        Vector2 blackHoleCenter = blackHoleRotationScript.transform.position - zombie.PlayerInfo.transform.position;
        blackHoleCenter.Normalize();
        zombie.PlayerInfo.GetComponent<Rigidbody2D>().velocity = blackHoleCenter*10;
        zombie.deltaTime += Time.deltaTime;
        if (zombie.deltaTime > 20)
        {
            zombie.Animator.SetBool("SkillBlack",false);
            zombie.BlackHoleSkillPrepeb.SetActive(false);
            zombie.ChangeState(zombie.idle);
        }
    }
}


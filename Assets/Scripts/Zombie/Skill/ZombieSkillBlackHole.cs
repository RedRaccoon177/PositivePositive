using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSkillBlackHole : ZombieState
{
    RotationCircle blackHoleRotationScript;
    int blackHoleSpeed;
    int blackHoleDuration;
    //플레이어와 블랙홀 사이 길이
    float distance;
    int blackHoleForceRange;
    int randX;
    int randY;


    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("SkillBlack",true);
        // 블랙홀 지속시간
        blackHoleDuration = 4;
        blackHoleSpeed = 10;
        blackHoleForceRange = 15;
        // 블랙홀 생성 후 위치시킴
        BlackHoleCreat(zombie);
        // 블랙홀 주변 도는 원
        BlackHoleRotationCircle(zombie);
    }

    public override void FixUpdate(ZombieController zombie)
    {
        zombie.deltaTime += Time.deltaTime;
        distance = Vector3.Distance(zombie.PlayerInfo.transform.position, zombie.BlackHoleSkillPrepeb.transform.position);
        if (distance < blackHoleForceRange)
        {
            Vector2 blackHoleCenter = blackHoleRotationScript.transform.position - zombie.PlayerInfo.transform.position;
            blackHoleCenter.Normalize();
            zombie.PlayerInfo.GetComponent<Rigidbody2D>().velocity = blackHoleCenter * blackHoleSpeed  ;
        }
        BlackStateToIdle(zombie);
    }
    public void BlackHoleCreat(ZombieController zombie)
    {
        randX = Random.Range(5, 28);
        randY = Random.Range(-10, 6);
        zombie.BlackHoleSkillPrepeb.transform.position = new Vector2(randX, randY);
        zombie.BlackHoleSkillPrepeb.SetActive(true);
    }
    public void BlackHoleRotationCircle(ZombieController zombie)
    {
        blackHoleRotationScript = zombie.BlackHoleSkillPrepeb.transform.GetChild(0).GetComponent<RotationCircle>();
        blackHoleRotationScript.centerRotation = zombie.BlackHoleSkillPrepeb;
        blackHoleRotationScript.rotationSpeed = 200f;
        blackHoleRotationScript.radius = 1f;
    }
    public void BlackStateToIdle(ZombieController zombie)
    {
        if (zombie.deltaTime > blackHoleDuration)
        {
            zombie.Animator.SetBool("SkillBlack", false);
            zombie.BlackHoleSkillPrepeb.SetActive(false);
            zombie.ChangeState(zombie.idle);
        }
    }
}


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
    int randX;
    int randY;


    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("SkillBlack",true);
        // 블랙홀 지속시간
        blackHoleDuration = 4;
        blackHoleSpeed = 30;
        // 블랙홀 생성 후 위치시킴
        BlackHoleCreat(zombie);
        // 블랙홀 주변 도는 원
        BlackHoleRotationCircle(zombie);
    }

    public override void FixUpdate(ZombieController zombie)
    {
        Debug.Log(distance);
        zombie.deltaTime += Time.deltaTime;
        distance = Vector3.Distance(zombie.PlayerInfo.transform.position, zombie.BlackHoleSkillPrepeb.transform.position);
        if (distance < 2.6f)
        {
            // 플레이어 체력 -1
        }
        if (distance < 20)
        {
            //if (distance <5)
            //{
            //    distance = 5;
            //}
            Vector2 blackHoleCenter = blackHoleRotationScript.transform.position - zombie.PlayerInfo.transform.position;
            blackHoleCenter.Normalize();
            if (Mathf.Abs(zombie.PlayerInfo.GetComponent<Rigidbody2D>().velocity.x) < 1.5 || Mathf.Abs(zombie.PlayerInfo.GetComponent<Rigidbody2D>().velocity.y) < 1.5)
            {
            zombie.PlayerInfo.GetComponent<Rigidbody2D>().velocity = blackHoleCenter * blackHoleSpeed * Time.deltaTime * distance*2;
                
            }
            else 
            {
            zombie.PlayerInfo.GetComponent<Rigidbody2D>().velocity = blackHoleCenter * blackHoleSpeed * Time.deltaTime * distance;

            }
        }
        BlackStateToIdle(zombie);
    }
    public void BlackHoleCreat(ZombieController zombie)
    {
        randX = Random.Range(-15, 15);
        randY = Random.Range(-15, 15);
        zombie.BlackHoleSkillPrepeb.transform.position = new Vector2(randX, randY);
        zombie.BlackHoleSkillPrepeb.SetActive(true);
    }
    public void BlackHoleRotationCircle(ZombieController zombie)
    {
        blackHoleRotationScript = zombie.BlackHoleSkillPrepeb.transform.GetChild(0).GetComponent<RotationCircle>();
        blackHoleRotationScript.centerRotation = zombie.BlackHoleSkillPrepeb;
        blackHoleRotationScript.rotationSpeed = 200f;
        blackHoleRotationScript.radius = 2f;
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


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SuwakoSkill0_ShootingBullet : SuwakoState
{
    //몇번째 총알을 사용 할 것인지 변수
    int bulletNum = 0;
    //오브젝트 풀링
    SuwakoBulletPool pool;

    //발사대의 위치
    Transform startTransform;
    //날라갈 곳의 위치
    Transform targetTransform;
    float speedPower = 10;

    //각 발사 각도들
    int angle = 10;
    bool changeState = false;

    public override void Enter(SuwakoController suwako)
    {
        //스와코 애니메이션 attackCa실행
        suwako.animator.SetInteger("IsSkills", 1);
        pool = SuwakoBulletPool.bulletPoolInstace;

        angle = 10;
        changeState = false;
    }

    public override void Update(SuwakoController suwako)
    {
        if (changeState == true && suwako.isSkill0End == false)
        {
            suwako.ChangeState(suwako.landingState);
        }
        else if (suwako.isSkill0End == true && changeState == false)
        {
            ShootBulletSkill0(suwako);
        }
    }

    public void MoveToAttack(GameObject bullet)
    {
        Vector2 direction = (targetTransform.position - startTransform.position).normalized;

        bullet.GetComponent<Rigidbody2D>().AddForce(direction * speedPower, ForceMode2D.Impulse);
    }

    public void ShootBulletSkill0(SuwakoController suwako)
    {
        for (int i = 0; i < 16; i++)
        {
            //좌표들
            startTransform = suwako.childObjects[4].transform;
            targetTransform = suwako.childObjects[4].transform.GetChild(0).transform;

            //오브젝트풀링으로 총알 만듬
            GameObject bullet = pool.GetObject(bulletNum);
            bullet.transform.position = startTransform.position;
            if (suwako.RiORLe == -1)
            {
                startTransform.rotation = Quaternion.Euler(0, 0, (i * angle) - 50);
            }
            else if (suwako.RiORLe == 1)
            {
                startTransform.rotation = Quaternion.Euler(0, 0, -(i * angle) + 50);
            }
            else 
            {
                startTransform.rotation = Quaternion.Euler(0, 0, (i * angle) - 50);
            }
            MoveToAttack(bullet);
        }
        changeState = true;
    }
}

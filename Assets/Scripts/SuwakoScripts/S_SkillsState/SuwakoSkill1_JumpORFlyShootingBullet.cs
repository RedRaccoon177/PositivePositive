using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class SuwakoSkill1_JumpORFlyShootingBullet : SuwakoState
{
    //몇번째 총알을 사용 할 것인지 변수
    int bulletNum = 1;
    //오브젝트 풀링
    SuwakoBulletPool pool;

    //부모객체의 위치
    Transform parentTransform;
    //자식객체의 위치
    Transform childTransform;
    float speedPower = 10;

    //각 발사 각도들
    int angle = 10;
    bool changeState = false;

    int count;

    public override void Enter(SuwakoController suwako)
    {
        suwako.animator.SetInteger("IsSkills", 2);
        pool = SuwakoBulletPool.bulletPoolInstace;
        angle = 10;
        changeState = false;
        count = 0;

        suwako.StartCoroutine(ShootBulletSkill1(suwako));
    }

    public override void Update(SuwakoController suwako)
    {
        if (changeState == true)
        {
            suwako.ChangeState(suwako.fallingState);
        }
        else if (changeState == false)
        {
            suwako.rb.velocity = new Vector2(0, 0);
        }
    }

    public void MoveToAttack(GameObject bullet)
    {
        Vector2 direction = (childTransform.position - parentTransform.position).normalized;

        bullet.GetComponent<Rigidbody2D>().AddForce(direction * speedPower, ForceMode2D.Impulse);
    }

    IEnumerator ShootBulletSkill1(SuwakoController suwako)
    {
        while (count < 5)
        {
            for (int i = 0; i < 36; i++)
            {
                
                //좌표들
                parentTransform = suwako.childObjects[5].transform;
                childTransform = suwako.childObjects[5].transform.GetChild(0).transform;

                //오브젝트풀링으로 총알 만듬
                GameObject bullet = pool.GetObject(bulletNum);
                bullet.transform.position = childTransform.position;
                parentTransform.rotation = Quaternion.Euler(0, 0, i * angle);

                MoveToAttack(bullet);
            }
            count++;
            yield return new WaitForSeconds(1f); // 1초 대기
        }
        changeState = true;
    }
}

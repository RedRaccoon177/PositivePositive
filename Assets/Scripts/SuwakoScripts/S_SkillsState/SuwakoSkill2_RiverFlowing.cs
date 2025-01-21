using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SuwakoSkill2_RiverFlowing : SuwakoState
{
    //몇번째 총알을 사용 할 것인지 변수
    int bulletNum = 2;
    //오브젝트 풀링
    SuwakoBulletPool pool;
    float speed = 20;

    //발사대의 위치
    Transform startTransform;
    //날라갈 곳의 위치
    Transform targetTransform;

    int angle = 10;

    //나뭇잎 자라게 하는 참거짓
    public bool isGrow = false;

    //코루틴 실행 중인가?
    public bool isCoroutine = false;

    //몇초정도 유지할까?
    float skillUseTime = 0;

    //코루틴 캐싱
    WaitForSeconds delay0 = new WaitForSeconds(0.1f);
    WaitForSeconds delay1 = new WaitForSeconds(0.4f);

    Vector3 targetScaleUP = new Vector3(10f, 10f, 10f); // 성장 최종 크기
    Vector3 targetScaleDown = new Vector3(0f, 0f, 0f);  // 성장 이후 다시 줄이기
    float duration = 1f; // 커지는 데 걸리는 시간


    public override void Enter(SuwakoController suwako)
    {
        skillUseTime = 0;

        pool = SuwakoBulletPool.bulletPoolInstace;

        //나뭇잎 크게 만들기
        suwako.StartCoroutine(ScaleOverTime(targetScaleUP, duration, suwako));

        //나뭇잎 커지게 실행
        isGrow = true;
        isCoroutine = true;
    }

    public override void Update(SuwakoController suwako)
    {
        if(isGrow == true && isCoroutine == true && suwako.isRiverSkill == true)
        {
            isCoroutine = false;
            suwako.StartCoroutine(Skill2Bullet2(suwako));
            suwako.StartCoroutine(Skill2Bullet0(suwako));
        }
        else if(isGrow == false && isCoroutine == false && suwako.isRiverSkill == false)
        {
            suwako.StartCoroutine(ScaleOverTime(targetScaleDown, duration, suwako));
        }
    }
    public void MoveToAttack(GameObject bullet)
    {
        Vector2 direction = (targetTransform.position - startTransform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
    IEnumerator Skill2Bullet2(SuwakoController suwako)
    {
        //1초에 while문 한번씩 실행
        while(skillUseTime < 6)
        {
            //좌표들
            startTransform = suwako.childObjects[6].transform;
            targetTransform = suwako.childObjects[6].transform.GetChild(0).transform;

            //오브젝트풀링으로 총알 만듬
            GameObject bullet = pool.GetObject(bulletNum);
            bullet.transform.position = startTransform.position;

            if (suwako.RiORLe == -1)
            {
                bullet.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (suwako.RiORLe == 1)
            {
                bullet.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                bullet.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            MoveToAttack(bullet);
            skillUseTime += 0.1f;
            yield return delay0;
        }

        //한 6초 뒤에 스킬 끝냄.
        isGrow = false;
        suwako.animator.SetInteger("IsRiverSkill", 2);
    }
    IEnumerator Skill2Bullet0(SuwakoController suwako)
    {
        while(skillUseTime < 6)
        {
            for (int i = 1; i < 16; i++)
            {
                //좌표들
                startTransform = suwako.childObjects[5].transform;
                targetTransform = suwako.childObjects[5].transform.GetChild(0).transform;

                GameObject bullets = pool.GetObject(0);
                bullets.transform.position = startTransform.position;

                if (suwako.RiORLe == -1)
                {
                    startTransform.rotation = Quaternion.Euler(0, 0, -i * angle);
                }
                else if (suwako.RiORLe == 1)
                {
                    startTransform.rotation = Quaternion.Euler(0, 0, i * angle);
                }
                else
                {
                    startTransform.rotation = Quaternion.Euler(0, 0, -i * angle);
                }
                MoveToAttack(bullets);
            }
            yield return delay1;
        }
    }


    IEnumerator ScaleOverTime(Vector3 target, float time, SuwakoController suwako)
    {
        Vector3 initialScale = suwako.childObjects[7].transform.localScale; // 현재 크기
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // 현재 시간 비율 계산
            float t = elapsedTime / time;

            // 선형 보간으로 크기 변경
            suwako.childObjects[7].transform.localScale = Vector3.Lerp(initialScale, target, t);

            elapsedTime += Time.deltaTime; // 시간 누적
            yield return null; // 다음 프레임까지 대기
        }

        // 최종 크기 설정 (정밀도 문제 방지)
        suwako.childObjects[7].transform.localScale = target;

        if (isGrow == true)
        {
            suwako.animator.SetInteger("IsRiverSkill", 1);
        }
        else if (isGrow == false)
        {
            suwako.ChangeState(suwako.idleState);
        }
    }
}



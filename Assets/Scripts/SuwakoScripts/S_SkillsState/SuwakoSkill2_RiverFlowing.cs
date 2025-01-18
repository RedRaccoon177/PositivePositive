using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SuwakoSkill2_RiverFlowing : SuwakoState
{
    ////몇번째 총알을 사용 할 것인지 변수
    //int bulletNum = 0;
    ////오브젝트 풀링
    //SuwakoBulletPool pool;

    ////발사대의 위치
    //Transform startTransform;
    ////날라갈 곳의 위치
    //Transform targetTransform;
    //float speed = 10;

    ////코루틴 캐싱
    //WaitForSeconds delay = new WaitForSeconds(0.8f);

    //public override void Enter(SuwakoController suwako)
    //{
    //    suwako.StartCoroutine(ShootBulletSkill0(suwako));

    //    pool = SuwakoBulletPool.bulletPoolInstace;

    //    //스와코 애니메이션 attackCa실행
    //    suwako.animator.SetTrigger("IsSkill0");

    //    //suwako.StopCoroutine(ShootBulletSkill0(suwako));
    //}

    //public void MoveToAttack(GameObject bullet)
    //{
    //    Vector2 direction = (targetTransform.position - startTransform.position).normalized;
    //    bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
    //}

    //IEnumerator ShootBulletSkill0(SuwakoController suwako)
    //{
    //    while (true)
    //    {
    //        yield return delay;
    //        // 풀에서 오브젝트 가져오기
    //        for (int i = 0; i < 7; i++)
    //        {
    //            //좌표들
    //            startTransform = suwako.gameObject.transform.GetChild(4);
    //            targetTransform = suwako.gameObject.transform.GetChild(4).transform.GetChild(0);

    //            //오브젝트풀링으로 총알 만듬
    //            GameObject bullet = pool.GetObject(bulletNum);
    //            bullet.transform.position = startTransform.position;
    //            startTransform.rotation = Quaternion.Euler(0, 0, i * 10);

    //            MoveToAttack(bullet);
    //        }
    //    }
    //}
}

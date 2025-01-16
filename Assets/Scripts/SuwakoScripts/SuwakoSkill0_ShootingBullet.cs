using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SuwakoSkill0_ShootingBullet : SuwakoState
{
    public GameObject prefab;
    private Queue<GameObject> pool = new Queue<GameObject>(); // 풀 저장소

    public override void Enter(SuwakoController suwako)
    {
        prefab = suwako.bullets[0];
        //스와코 애니메이션 attackCa실행
    }

    public override void Update(SuwakoController suwako)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 풀에서 오브젝트 가져오기
            GameObject bullet = GetObject();

            //발사할 위치
            bullet.transform.position = suwako.bullet0Fire.transform.position;
        }
    }

    public void OnBulletDestroy(GameObject bullet)
    {
        // 오브젝트 풀로 반환
        ReturnObject(bullet);
    }

    
    // 풀에서 오브젝트 가져오기
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            var obj = pool.Dequeue();
            obj.SetActive(true); // 오브젝트 활성화
            return obj;
        }

        // 풀에 오브젝트가 없으면 새로 생성
        return GameObject.Instantiate(prefab);
    }

    // 오브젝트 풀로 반환하기
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 오브젝트 비활성화
        pool.Enqueue(obj); // 풀에 반환
    }
}

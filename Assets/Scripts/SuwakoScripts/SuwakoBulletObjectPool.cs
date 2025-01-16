using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoBulletObjectPool : MonoBehaviour
{
    public GameObject prefab; // 재사용할 프리팹

    [SerializeField]

    private Queue<GameObject> pool = new Queue<GameObject>(); // 풀 저장소

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
        return Instantiate(prefab);
    }

    // 오브젝트 풀로 반환하기
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 오브젝트 비활성화
        pool.Enqueue(obj); // 풀에 반환
    }
}

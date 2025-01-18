using System.Collections.Generic;
using UnityEngine;

public class ZombieObjectPooling : MonoBehaviour
{
    public GameObject wormprefab; // 재사용할 프리팹
    private Queue<GameObject> pool = new Queue<GameObject>(); // 풀 저장소
    public int poolMaxCount = 4;

    // 오브젝트 비활서화 후 pool에 담기
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 오브젝트 비활성화
        pool.Enqueue(obj); // 풀에 반환
    }
    /// <summary>
    /// 모든 Worm들 인스턴스화 시키고 비활성화
    /// </summary>
    public void CreatObject()
    {
        if (wormprefab == null)
        {
            Debug.Log("null");
        }
        for (int i = 0; i <  poolMaxCount; i++)
        {
            var obj = Instantiate(wormprefab);
            obj.SetActive(false);
            obj.GetComponent<WormRotation>().zombieInfo = gameObject;
            obj.GetComponent<WormRotation>().num = i;
            obj.GetComponent<WormRotation>().objectPool = this;
            //StartSetting(obj, i);
            pool.Enqueue(obj);
        }
    }
    /// <summary>
    /// 모든 Worm들 활성화
    /// </summary>
    public void AllActiveTrue()
    {
           // Debug.Log(pool.Count);    
        if (pool.Count < 4)
        {
            return;
        }
        else if (pool.Count == 4)
        {
            for (int i =0; i< poolMaxCount; i++)
            {
                var obj = pool.Dequeue();
                obj.SetActive(true);
                StartSetting(obj, i);
            }
        }
    }
    /// <summary>
    /// 몬스터 주변을 도는 worm들 90, 180, 270, 360도 위치에 생성되게 하는 함수
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="i"> 순서 </param>
    public void StartSetting(GameObject obj, int i)
    {
        obj.GetComponent<WormRotation>().CreatWormShield(i);
        obj.transform.position = new Vector2(obj.GetComponent<WormRotation>().angleX, obj.GetComponent<WormRotation>().angleY);

    }
}

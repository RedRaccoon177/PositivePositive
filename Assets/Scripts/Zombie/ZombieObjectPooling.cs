using System.Collections.Generic;
using UnityEngine;

public class ZombieObjectPooling : MonoBehaviour
{
    public GameObject wormPrefab;// 재사용할 프리팹
    public Queue<GameObject> pool = new Queue<GameObject>(); // 풀 저장소
    public int poolMaxCount = 4;

    /// <summary>
    /// 오브젝트 비활성화 후 pool에 담기
    /// </summary>
    /// <param name="obj"></param>
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
        if (wormPrefab == null)
        {
            Debug.Log("null");
        }
        for (int i = 0; i <  poolMaxCount; i++)
        {
            var obj = Instantiate(wormPrefab,gameObject.transform);
            obj.SetActive(false);
            obj.GetComponent<RotationCircle>().centerRotation = gameObject;
            obj.GetComponent<RotationCircle>().num = i;
            obj.GetComponent<RotationCircle>().radius = 5f;
            obj.GetComponent<RotationCircle>().rotationSpeed = 50f;
            obj.GetComponent<WormController>().objPooling = this;
            //obj.GetComponent<WormController>().zombieii= gameObject;
            //obj.GetComponent<RotationCircle>().objectPool = this;
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
    public void AllActiveFalse()
    {
        Debug.Log("아ㅏㅏ");
        for (int i = 2; i < poolMaxCount + 2; i++)
        {
            Debug.Log(gameObject.transform.GetChild(i).gameObject.name  + ": i");
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
            pool.Enqueue(gameObject.transform.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// 몬스터 주변을 도는 worm들 90, 180, 270, 360도 위치에 생성되게 하는 함수
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="i"> 순서 </param>
    public void StartSetting(GameObject obj, int i)
    {
        obj.GetComponent<RotationCircle>().CreatWormShield(i);
        obj.transform.position = new Vector2(obj.GetComponent<RotationCircle>().angleX, obj.GetComponent<RotationCircle>().angleY);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoBulletPool : MonoBehaviour
{
    public static SuwakoBulletPool bulletPoolInstace;

    [SerializeField]
    public GameObject[] bullets; // 재사용할 프리팹

    //풀저장소도 프리펩에 의해 갯수 늘릴수 있도록 설계해야함.
    private Queue<GameObject>[] pools; // 풀 저장소

    private void Start()
    {
        bulletPoolInstace = this;

        pools = new Queue<GameObject>[bullets.Length];

        for (int i = 0; i < bullets.Length; i++)
        {
            // 각 큐 초기화
            pools[i] = new Queue<GameObject>();
        }

        //각 100개씩 만듬과 동시에, 풀 저장소에 넣는다.
        for (int i = 0; i < 100; i++)
        {
            for(int bulletNums = 0; bulletNums < bullets.Length; bulletNums++)
            {
                var t = Instantiate(bullets[bulletNums]);
                t.SetActive(false);
                pools[bulletNums].Enqueue(t);
            }
        }
    }

    // 풀에서 오브젝트 가져오기
    public GameObject GetObject(int bulletNum)
    {
        if (pools[bulletNum].Count > 0)
        {
            var obj = pools[bulletNum].Dequeue();
            obj.SetActive(true); // 오브젝트 활성화
            return obj;
        }

        // 풀에 오브젝트가 없으면 새로 생성
        return Instantiate(bullets[bulletNum]);
    }

    // 오브젝트 풀로 반환하기
    public void ReturnObject(GameObject obj, int k)
    {
        obj.SetActive(false); // 오브젝트 비활성화
        pools[k].Enqueue(obj); // 풀에 반환
    }
}

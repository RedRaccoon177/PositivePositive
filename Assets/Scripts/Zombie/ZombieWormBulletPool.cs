using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWormBulletPool : MonoBehaviour
{
    public GameObject wormBulletPrefeb;
    public Queue<GameObject> wormBulletPool = new Queue<GameObject>(); 
    public int poolMaxCount = 30;
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 오브젝트 비활성화
        wormBulletPool.Enqueue(obj); // 풀에 반환
    }
    public void WormBulletActiveTrue(ZombieController zombie)
    {
        Debug.Log("활성화");
        if (wormBulletPool.Count >0)
        {
            var obj = wormBulletPool.Dequeue();
            obj.SetActive(true);
            obj.transform.position = zombie.transform.position;
        }
    }
    public void CreatObject()
    {
        if (wormBulletPrefeb == null)
        {
            Debug.Log("null");
        }
        for (int i = 0; i < poolMaxCount; i++)
        {
            var obj = Instantiate(wormBulletPrefeb);
            //obj.GetComponent<WormBulletController>().zombieInfo = gameObject;
            obj.GetComponent<WormBulletController>().playerInfo = gameObject.GetComponent<ZombieController>().PlayerInfo;
            obj.GetComponent<WormBulletController>().zombieBulletPool = this;
            obj.SetActive(false);
            wormBulletPool.Enqueue(obj);
        }
    }
}

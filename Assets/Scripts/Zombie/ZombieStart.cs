using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStart : ZombieState
{
    GameObject zombieStartImage;
    float startTime;
    float startTime2;
    float startTime3;
    public override void Enter(ZombieController zombie)
    {
        zombieStartImage = GameObject.Find("ZombieStartImage");
        zombieStartImage.SetActive(false);
        zombie.transform.position = Vector3.zero;
       // zombieStartImage.SetActive(true);
    }
    public override void Update(ZombieController zombie)
    {
        startTime += Time.deltaTime;
        startTime2 += Time.deltaTime;
        startTime3  += Time.deltaTime;
        if (startTime2 > 1)
        {
            Camera.main.GetComponent<CameraMove>().ZoomBoss(zombie.gameObject,true);
            if (startTime3 >3)
            {
                zombieStartImage.SetActive(true);
            }
        }
        if (startTime > 6 || Input.GetKey(KeyCode.Space))
        {
            Debug.Log("시작!!");
            Camera.main.GetComponent<CameraMove>().ZoomBoss(zombie.gameObject, false);
            zombie.Animator.SetBool("IsStart",true);
            zombieStartImage.SetActive(false);
            zombie.ChangeState(zombie.idle);
        }

    }
}

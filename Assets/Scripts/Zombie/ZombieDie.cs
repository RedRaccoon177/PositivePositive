using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDie : ZombieState
{
    GameObject gameOver;
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsDie",true);
    }
    public override void Update(ZombieController zombie)
    {
        if (zombie.isDie == true)
        {
            zombie.transform.position = Vector3.zero;
            zombie.transform.rotation = Quaternion.identity;
            Camera.main.GetComponent<CameraMove>().ZoomBoss(zombie.gameObject,true);
            //GameManager.Instance.Victory();
        }
    }
}

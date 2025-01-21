using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHPObserver : MonoBehaviour
{
    public event Action<float> OnHealthChanged;

    //맞았을 때 보스에서 옵저버 GetComponent해서 이 메소드 부르기
    public void NotifyHealthChange(float maxHP, float curHP)
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(curHP / maxHP);
        }
    }
}

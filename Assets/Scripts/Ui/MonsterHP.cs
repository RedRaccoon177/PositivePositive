using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : MonoBehaviour
{
    public Image healthBarFill; // 체력바의 Fill 영역
    //MonsterHPObserver 보스 오브젝트에 붙이고 보스에서 연결해줘야함
    public MonsterHPObserver observer; // Subject (체력 시스템)

    private void Start()
    {

        // 체력 변화 이벤트 구독
        if (observer != null)
        {
            observer.OnHealthChanged += UpdateHealthBar;
        }
    }


    void OnDisable()
    {
        // 체력 변화 이벤트 구독 해지
        if (observer != null)
        {
            observer.OnHealthChanged -= UpdateHealthBar;
        }
    }

    // 체력바 업데이트
    private void UpdateHealthBar(float healthPercent)
    {
        if (healthBarFill != null)
        {
            Debug.Log(healthPercent);
            healthBarFill.fillAmount = healthPercent; // fillAmount 값 적용
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : MonoBehaviour
{
    public Image healthBarFill; // 체력바의 Fill 영역
    public SuwakoGetHitState getHitState; // Subject (체력 시스템)

    public GameObject suwako;


    private void Start()
    {
        getHitState = suwako.GetComponent<SuwakoController>().GetHitState;

        // 체력 변화 이벤트 구독
        if (getHitState != null)
        {
            getHitState.OnHealthChanged += UpdateHealthBar;
        }
    }


    void OnDisable()
    {
        // 체력 변화 이벤트 구독 해지
        if (getHitState != null)
        {
            getHitState.OnHealthChanged -= UpdateHealthBar;
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

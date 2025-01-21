using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHp : MonoBehaviour
{
    public Image healthBarFill; // 체력바의 Fill 영역
    public ZombieHitted getHitState; // Subject (체력 시스템)

    public GameObject zombie;


    private void Start()
    {
        getHitState = zombie.GetComponent<ZombieController>().zombieHitted;

        if (getHitState != null)
        {
            Debug.Log("getHitState : " + getHitState);
            // 이벤트 발생시 이거 실행해라
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
        Debug.Log("함수 들어옴 : " + healthBarFill);
        if (healthBarFill != null)
        {
            Debug.Log("HealthBarFill" + healthBarFill);
            Debug.Log(healthPercent);
            healthBarFill.fillAmount = healthPercent; // fillAmount 값 적용
        }
    }
}

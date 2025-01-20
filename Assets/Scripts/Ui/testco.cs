using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f; // 최대 체력
    private float currentHealth;

    // 체력 변화 알림을 위한 이벤트
    public event Action<float> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth; // 초기 체력 설정
    }

    // 데미지를 받을 때 호출
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력을 0~최대값 사이로 제한
        NotifyHealthChanged();
    }


    // 옵저버들에게 체력 변화를 알림
    private void NotifyHealthChanged()
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(currentHealth / maxHealth); // 체력 비율을 전달
        }
    }
}

public class HealthBarUI : MonoBehaviour
{
    public Image healthBarFill; // 체력바의 Fill 영역
    public HealthSystem healthSystem; // Subject (체력 시스템)

    void OnEnable()
    {
        // 체력 변화 이벤트 구독
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged += UpdateHealthBar;
        }
    }

    void OnDisable()
    {
        // 체력 변화 이벤트 구독 해지
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged -= UpdateHealthBar;
        }
    }

    // 체력바 업데이트 메서드
    private void UpdateHealthBar(float healthPercent)
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = healthPercent; // 체력 비율에 따라 Fill Amount 조정
        }
    }
}

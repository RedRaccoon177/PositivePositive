using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTextUI : MonoBehaviour
{
    public TextMeshProUGUI tmpText; // 텍스트 프로 오브젝트 연결
    [SerializeField] float fadeDuration = 2.0f; // 밝아지거나 어두워지는 데 걸리는 시간 (초)

    float timer = 0f; // 시간 누적용 변수
    bool isFadingOut = false; // 현재 어두워지는 중인지 여부

    void Update()
    {
        if (tmpText == null)
        {
            Debug.LogError("TextMeshProUGUI가 연결되지 않았습니다!");
            return;
        }

        // 타이머 증가
        timer += Time.deltaTime;

        // 밝기 비율 계산 (0 ~ 1)
        float alpha = isFadingOut
            ? Mathf.Lerp(1f, 0.2f, timer / fadeDuration) // 어두워지는 과정
            : Mathf.Lerp(0.2f, 1f, timer / fadeDuration); // 밝아지는 과정

        // 알파 값 적용
        tmpText.color = new Color(tmpText.color.r, tmpText.color.g, tmpText.color.b, alpha);

        // 페이딩 완료 시 방향 전환 및 타이머 초기화
        if (timer >= fadeDuration)
        {
            isFadingOut = !isFadingOut; // 방향 전환
            timer = 0f; // 타이머 초기화
        }
    }
}


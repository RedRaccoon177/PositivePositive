using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrageGrow : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(10f, 10f, 10f); // 최종 크기
    public float duration = 1f; // 커지는 데 걸리는 시간

    void Start()
    {
        StartCoroutine(ScaleOverTime(targetScale, duration));
    }

    IEnumerator ScaleOverTime(Vector3 target, float time)
    {
        Vector3 initialScale = transform.localScale; // 현재 크기
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // 현재 시간 비율 계산
            float t = elapsedTime / time;

            // 선형 보간으로 크기 변경
            transform.localScale = Vector3.Lerp(initialScale, target, t);

            elapsedTime += Time.deltaTime; // 시간 누적
            yield return null; // 다음 프레임까지 대기
        }

        // 최종 크기 설정 (정밀도 문제 방지)
        transform.localScale = target;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrageGrow : MonoBehaviour
{
    Vector3 targetScaleUP = new Vector3(10f, 10f, 10f); // 성장 최종 크기
    Vector3 targetScaleDown = new Vector3(0f, 0f, 0f);  // 성장 이후 다시 줄이기
    float duration = 1f; // 커지는 데 걸리는 시간

    public bool isGrow = true;

    SuwakoController suwako;

    void Start()
    {
        suwako = GetComponent<SuwakoController>();
        isGrow = true;
    }

    void Update()
    {
        if (isGrow == true)
        {
            StartCoroutine(ScaleOverTime(targetScaleUP, duration));
        }
        else if (isGrow == false)
        {
            StartCoroutine(ScaleOverTime(targetScaleDown, duration));
        }
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

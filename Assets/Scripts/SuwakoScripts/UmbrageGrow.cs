using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrageGrow : MonoBehaviour
{
    Vector3 targetScaleUP = new Vector3(10f, 10f, 10f); // 성장 최종 크기
    Vector3 targetScaleDown = new Vector3(0f, 0f, 0f);  // 성장 이후 다시 줄이기
    float duration = 1f; // 커지는 데 걸리는 시간

    public GameObject suwako;

    SuwakoSkill2_RiverFlowing skill2_RiverFlowing;


    void Start()
    {
        skill2_RiverFlowing = suwako.GetComponent<SuwakoController>().skill2_RiverFlowing;
    }

    void Update()
    {
        if (skill2_RiverFlowing.isGrow == true)
        {
            suwako.GetComponent<SuwakoController>().animator.SetInteger("IsRiverSkill", 1);
            StartCoroutine(ScaleOverTime(targetScaleUP, duration, suwako));
        }
        else if (skill2_RiverFlowing.isGrow == false)
        {
            suwako.GetComponent<SuwakoController>().childObjects[7].GetComponent<UmbrageGrow>().enabled = false;
            StartCoroutine(ScaleOverTime(targetScaleDown, duration, suwako));
        }
    }

    IEnumerator ScaleOverTime(Vector3 target, float time, GameObject suwako)
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

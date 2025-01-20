using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBulletFadeEffect : MonoBehaviour
{
    [SerializeField]
    float duration = 1f; // 선명해지는 데 걸리는 시간

    private Renderer[] childRenderers; // 모든 자식의 Renderer 배열

    void Start()
    {
        // 자식 오브젝트의 Renderer 컴포넌트를 모두 가져오기
        childRenderers = GetComponentsInChildren<Renderer>();

        // 모든 자식 오브젝트의 색상을 초기화
        foreach (var renderer in childRenderers)
        {
            Color startColor = renderer.material.color;
            startColor.a = 0f; // 알파값(투명도)을 0으로 설정
            renderer.material.color = startColor;
        }

        // 코루틴 시작
        StartCoroutine(FadeIn(duration));
    }

    IEnumerator FadeIn(float time)
    {
        float elapsedTime = 0f;

        // 초기 색상과 목표 색상을 각각 저장
        Dictionary<Renderer, Color> initialColors = new Dictionary<Renderer, Color>();
        Dictionary<Renderer, Color> targetColors = new Dictionary<Renderer, Color>();

        foreach (var renderer in childRenderers)
        {
            // 초기 색상 저장
            initialColors[renderer] = renderer.material.color;

            // 목표 색상 복사 후 알파값 설정
            Color targetColor = renderer.material.color;
            targetColor.a = 1f; // 알파값 1로 설정
            targetColors[renderer] = targetColor;
        }

        while (elapsedTime < time)
        {
            float t = elapsedTime / time; // 비율 계산 (0 ~ 1)

            // 모든 Renderer의 색상을 선형 보간으로 변경
            foreach (var renderer in childRenderers)
            {
                renderer.material.color = Color.Lerp(initialColors[renderer], targetColors[renderer], t);
            }

            elapsedTime += Time.deltaTime; // 시간 누적
            yield return null; // 다음 프레임까지 대기
        }

        // 모든 Renderer의 최종 색상 설정
        foreach (var renderer in childRenderers)
        {
            renderer.material.color = targetColors[renderer];
        }
    }

}

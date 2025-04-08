using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    float duration = 1f; // 선명해지는 데 걸리는 시간
    public Button menuBtn;
    public Button retryBtn;
    private Image[] childImages; // 모든 자식의 Image 배열

    void Start()
    {
        // 자식 오브젝트의 Image 컴포넌트를 모두 가져오기
        childImages = GetComponentsInChildren<Image>();
        //이거는 인자 없는 메소드 달아줄 때
        menuBtn.onClick.AddListener(SceneChanger.Instance.MoveToTitleScene);
        //이거는 인자 있는 메소드 달아야 될 때 쓰는 람다식
        retryBtn.onClick.AddListener(() => SceneChanger.Instance.ChangeSceneWithLoad(""));

        // 모든 자식 오브젝트의 색상을 초기화
        foreach (var Image in childImages)
        {
            Color startColor = Image.color;
            startColor.a = 0f; // 알파값(투명도)을 0으로 설정
            Image.color = startColor;
        }

        StartCoroutine(FadeIn(duration));
        // 코루틴 시작
    }

    IEnumerator FadeIn(float time)
    {
        float elapsedTime = 0f;

        // 초기 색상과 목표 색상을 각각 저장
        Dictionary<Image, Color> initialColors = new Dictionary<Image, Color>();
        Dictionary<Image, Color> targetColors = new Dictionary<Image, Color>();

        foreach (var Image in childImages)
        {
            // 초기 색상 저장
            initialColors[Image] = Image.color;

            // 목표 색상 복사 후 알파값 설정
            Color targetColor = Image.color;
            targetColor.a = 1f; // 알파값 1로 설정
            targetColors[Image] = targetColor;
        }

        while (elapsedTime < time)
        {
            float t = elapsedTime / time; // 비율 계산 (0 ~ 1)

            // 모든 Image 색상을 선형 보간으로 변경
            foreach (var Image in childImages)
            {
                Image.color = Color.Lerp(initialColors[Image], targetColors[Image], t);
            }

            elapsedTime += Time.deltaTime; // 시간 누적
            yield return null; // 다음 프레임까지 대기
        }

        // 모든 Image 최종 색상 설정
        foreach (var Image in childImages)
        {
            Image.color = targetColors[Image];
        }
    }
}

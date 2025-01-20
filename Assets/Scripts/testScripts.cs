using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScripts : MonoBehaviour
{
    public Transform target; // 이동할 목표 지점
    public float duration = 5f; // 이동에 걸릴 시간

    void Start()
    {
        StartCoroutine(MoveOverTime(target.position, duration));
    }

    IEnumerator MoveOverTime(Vector3 destination, float time)
    {
        Vector3 start = transform.position; // 현재 위치
        float elapsed = 0f; // 경과 시간

        while (elapsed < time)
        {
            transform.position = Vector3.Lerp(start, destination, elapsed / time); // 점진적으로 이동
            elapsed += Time.deltaTime; // 경과 시간 누적
            yield return null; // 다음 프레임까지 대기
        }

        transform.position = destination; // 정확히 도착 지점으로 이동
    }
}

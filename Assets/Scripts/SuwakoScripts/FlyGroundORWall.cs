using UnityEngine;

public class FlyGroundORWall : MonoBehaviour
{
    public float amplitude = 1f; // 이동 범위 (위아래 움직이는 거리)
    public float frequency = 1f; // 이동 속도 (주기)
    private Vector3 startPosition;

    void Start()
    {
        // 초기 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        Debug.Log("Time.time: " + Time.time);
        // 천천히 위아래로 움직이기
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}


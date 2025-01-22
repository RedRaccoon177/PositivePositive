using UnityEngine;

public class FlyGroundORWall : MonoBehaviour
{
    public float amplitude = 1f; // 이동 범위 (위아래 움직이는 거리)
    public float frequency = 1f; // 이동 속도 (주기)
    private Vector3 startPosition;
    public bool xORy = false;

    void Start()
    {
        // 초기 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        if (xORy == false)
        {
            // 천천히 위아래로 움직이기
            float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = startPosition + new Vector3(0, yOffset, 0);
        }
        else
        {
            // 천천히 위아래로 움직이기
            float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = startPosition + new Vector3(yOffset, 0, 0);
        }
    }
}

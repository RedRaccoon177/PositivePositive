using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    GameObject boss;
    bool zoomPlayer = true;
    public float zoomSize;

    [Tooltip("카메라 바운더리 설정")]
    [System.Serializable]
    public struct CameraLimit
    {
        [Tooltip("맵 중심점 좌표 설정")]
        public Vector2 center;
        [Tooltip("맵의 크기 설정 (씬뷰에서 빨간선으로 크기 그려질거임")]
        public Vector2 mapSize;

        [HideInInspector]
        public float height;
        [HideInInspector]
        public float width;
    }

    public CameraLimit cameraLimit;

    // Start is called before the first frame update
    void Start()
    {
        cameraLimit.height = Camera.main.orthographicSize;
        cameraLimit.width = cameraLimit.height * Screen.width / Screen.height; //세로 * (세로에 대한 가로의 비율)
        player = GameObject.FindWithTag("Player");
    }

    //true면 bossObj에 카메라를 옮기고 zoomSize 만큼 카메라를 확대함, false면 카메라를 플레이어에게 옮기고 확대율을 초기값으로
    void Update()
    {
        if (zoomPlayer)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(player.transform.position.x, player.transform.position.y + 2, -10), Time.deltaTime * 3);
            if (Camera.main.orthographicSize <= cameraLimit.height - 0.05f)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraLimit.height, Time.deltaTime * 3);
            }
            else if (Camera.main.orthographicSize != cameraLimit.height)
            {
                Camera.main.orthographicSize = cameraLimit.height;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(boss.transform.position.x, boss.transform.position.y, -10), Time.deltaTime * 3);
            if (Camera.main.orthographicSize >= zoomSize + 0.05f)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomSize, Time.deltaTime * 3);
            }
            else if (Camera.main.orthographicSize != zoomSize)
            {
                Camera.main.orthographicSize = zoomSize;
            }
        }

        float limitX = cameraLimit.mapSize.x - cameraLimit.width;
        float clampX = Mathf.Clamp(transform.position.x, -limitX + cameraLimit.center.x, limitX + cameraLimit.center.x);

        float limitY = cameraLimit.mapSize.y - cameraLimit.height;
        float clampY = Mathf.Clamp(transform.position.y, -limitY + cameraLimit.center.y, limitY + cameraLimit.center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    //디버그로 설정한 맵 사이즈 보여주기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cameraLimit.center, cameraLimit.mapSize * 2);
    }

    //state가 true면 몬스터 따라가게, false면 플레이어
    public void ZoomBoss(GameObject bossObj, bool state)
    {
        if (state == true)
        {
            boss = bossObj;
        }
        else
        {
            boss = null;
        }
        zoomPlayer = state;
    }

    //얘는 그냥 버튼테스트용임
    public void ZoomBossTest(GameObject bossObj)
    {
        boss = bossObj;
        zoomPlayer = !zoomPlayer;
    }
}

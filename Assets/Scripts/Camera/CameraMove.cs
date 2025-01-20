using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    GameObject boss;
    bool zoomPlayer = true;
    float initCamSize;
    public float zoomSize;
    // Start is called before the first frame update
    void Start()
    {
        initCamSize = Camera.main.orthographicSize;
        player = GameObject.FindWithTag("Player");
    }

    //true면 bossObj에 카메라를 옮기고 zoomSize 만큼 카메라를 확대함, false면 카메라를 플레이어에게 옮기고 확대율을 초기값으로
    void Update()
    {
        if (zoomPlayer)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(player.transform.position.x, player.transform.position.y, -10), Time.deltaTime * 3);
            if (Camera.main.orthographicSize <= initCamSize - 0.05f)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, initCamSize, Time.deltaTime * 3);
            }
            else if (Camera.main.orthographicSize != initCamSize)
            {
                Camera.main.orthographicSize = initCamSize;
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

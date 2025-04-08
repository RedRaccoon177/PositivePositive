using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomUiSlug : MonoBehaviour
{
    bool startShow = true;

    public GameObject ui;
    public float fadeDuration = 1f; // 페이드 시간 (초)
    public SlugBoss slugBoss;

    private void Start()
    {
        // SpriteRenderer 가져오기
        ui = GameObject.Find("SulgBossUI");
        ui.SetActive(false);
        Camera.main.GetComponent<CameraMove>().ZoomBoss(slugBoss.gameObject, startShow);
        StartCoroutine(ShowUIStart(slugBoss));
    }

    IEnumerator ShowUIStart(SlugBoss slugBoss)
    {
        yield return new WaitForSeconds(1.2f);
        ui.SetActive(true);

        yield return new WaitForSeconds(3.5f);
        ui.SetActive(false);

        startShow = false;
        Camera.main.GetComponent<CameraMove>().ZoomBoss(slugBoss.gameObject, startShow);
    }
}

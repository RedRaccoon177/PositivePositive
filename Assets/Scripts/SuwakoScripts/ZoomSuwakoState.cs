using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomSuwakoState : SuwakoState
{
    bool startShow = true;

    public GameObject ui;
    public float fadeDuration = 1f; // 페이드 시간 (초)

    public override void Enter(SuwakoController suwako)
    {
        // SpriteRenderer 가져오기
        ui = GameObject.Find("SuwakoImage");
        ui.SetActive(false);
        Camera.main.GetComponent<CameraMove>().ZoomBoss(suwako.gameObject, startShow);
        suwako.StartCoroutine(ShowUIStart(suwako));
    }

    IEnumerator ShowUIStart(SuwakoController suwako)
    {
        yield return new WaitForSeconds(1.5f);
        ui.SetActive(true);

        yield return new WaitForSeconds(3.2f);
        ui.SetActive(false);

        startShow = false;
        Camera.main.GetComponent<CameraMove>().ZoomBoss(suwako.gameObject, startShow);
    }
}

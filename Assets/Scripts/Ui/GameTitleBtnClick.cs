using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameTitleBtnClick : MonoBehaviour 
{
    public GameObject PopUp;
    


    public void OnClickGamePlaye()
    {
        Debug.Log("시작 버튼 클릭");
        PopUp.SetActive(true);
    }
    public void OnClickGameExit()
    {
        Debug.Log("게임 종료 버튼 클릭");
    }
    public void OnClickBossFirst()
    {
        Debug.Log("보스1 클릭");
    }
    public void OnClickBossSecond()
    {
        Debug.Log("보스2 클릭");
    }
    public void OnClickBossThird()
    {
        Debug.Log("보스3 클릭");
    }
    public void OnClickPopUpExit()
    {
        PopUp.SetActive(false);
        Debug.Log("팝업 나가기");
    }
    public void OnClickpPauseExit()
    {
        PopUp.GetComponent<Stop>().ResumeGame();
    }

}

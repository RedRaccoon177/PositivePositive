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
        //종료 코드
        Application.Quit();
        Debug.Log("게임 종료 버튼 클릭");
    }
    public void OnClickBossFirst()
    {
        //이런 식으로 씬매니저 인스턴스에서 ChangeSceneWithLoad안에 씬 이름 넣으면 됨
        SceneChanger.Instance.ChangeSceneWithLoad("SuwakoBossSecen");
        Debug.Log("보스1 클릭");
    }
    public void OnClickBossSecond()
    {
        SceneChanger.Instance.ChangeSceneWithLoad("StageZombie");
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

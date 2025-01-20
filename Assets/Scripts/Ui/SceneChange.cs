using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public static SceneChange sceneChange;

    private void Awake()
    {
        if (sceneChange == null)
        {
            sceneChange = this;
        }  
        else
        {
            Destroy(sceneChange.gameObject);
        }
    }
    // 게임 시작 화면
    public void GameTitleScene()
    {

    }
    public void GameLoadingScene()
    {

    }
    public void GameOverScene()
    {

    }
    public void GameStage()
    {

    }
    public void GameStageZombie()
    {

    }
    public void GameStageSwako()
    {

    }
}

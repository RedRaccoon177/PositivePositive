using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance
    {
        get; private set;
    }

    string toLoad = "";
    Image loadingImg;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    //로드하고자하는 씬 이름 집어넣으면 로딩씬 -> 이름적은 씬으로 넘어감
    public void ChangeSceneWithLoad(string sceneName)
    {
        toLoad = sceneName;
        SceneManager.LoadScene("LoadingScene"); //Async였다? 보장 못함. Load 가능할수도?
        StartCoroutine(Loading());
    }

    //로딩바 돌려주는 코루틴
    IEnumerator Loading()
    {
        yield return null;
        loadingImg = GameObject.Find("LoadingProgress").GetComponent<Image>();
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(toLoad);
        asyncOp.allowSceneActivation = false;
        float timer = 0.0f;
        while (!asyncOp.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (asyncOp.progress < 0.9f)
            {
                loadingImg.fillAmount = Mathf.Lerp(loadingImg.fillAmount, asyncOp.progress, timer);
                if (loadingImg.fillAmount >= asyncOp.progress)
                {
                    timer = 0.0f;
                }
            }
            else
            {
                loadingImg.fillAmount = Mathf.Lerp(loadingImg.fillAmount, 1, timer);
                if (loadingImg.fillAmount >= 1)
                {
                    asyncOp.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    //타이틀씬으로 이동하기
    public void MoveToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

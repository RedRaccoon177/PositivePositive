using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button titleBtn;
    public GameObject gameOverUI;
    public GameObject victoryUI;

    // Start is called before the first frame update
    void Start()
    {
        titleBtn.onClick.AddListener(SceneChanger.Instance.MoveToTitleScene);
        GameManager.Instance.SetUIManager(this);
    }

    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    public void VictoryUI()
    {
        victoryUI.SetActive(true);
    }
}

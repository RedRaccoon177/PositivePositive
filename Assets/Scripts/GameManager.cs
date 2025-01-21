using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager uiManager;
    public static GameManager Instance
    {
        get; private set;
    }

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

    public void SetUIManager(UIManager ui)
    {
        uiManager = ui;
    }

    public void GameOver()
    {
        uiManager.GameOverUI();

    }

    public void Victory()
    {
        uiManager.VictoryUI();
    }
}

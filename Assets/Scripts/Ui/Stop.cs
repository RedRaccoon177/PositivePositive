using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    public GameObject stopUi;
    bool isPaused;
    private void Start()
    {
        isPaused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                ResumeGame();
            }
            else if (isPaused == false)
            {
                PauseGame();
            }
        }
    }
    // 게임 스탑
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        stopUi.SetActive(true);
        isPaused = true;
    }
    // 게임 재개
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        stopUi.SetActive(false);
        isPaused = false;
    }
}

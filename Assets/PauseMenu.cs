using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject failPanel;

    [SerializeField]
    GameObject WinPanel;

    [SerializeField]
    Constant gameConstants;

    bool hasWon = false;
    private int delayCount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && gameConstants.HasFailed == false && hasWon == false)
        {
            pausePanel.SetActive(!pausePanel.activeInHierarchy);
            gameConstants.IsPaused = pausePanel.activeInHierarchy;
        }

        if(!gameConstants.IsPaused)
        {
            if (hasWon)
            {
                if (Time.frameCount - delayCount > gameConstants.PostWinDelay)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    public void WonLevel()
    {
        if (SceneManager.GetActiveScene().name != "Level 3")
        {
            hasWon = true;
            delayCount = Time.frameCount;
        }
        else
        {
            WinPanel.SetActive(true);
        }
    }

    public void FailedLevel()
    {
        failPanel.SetActive(true);
        gameConstants.IsPaused = true;
        gameConstants.HasFailed = true;
    }

    public void ExitLevel()
    {
        gameConstants.IsPaused = false;
        gameConstants.HasFailed = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void ResetLevel()
    {
        gameConstants.IsPaused = false;
        gameConstants.HasFailed = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetfromStart()
    {
        gameConstants.IsPaused = false;
        gameConstants.HasFailed = false;
        SceneManager.LoadScene(1);
    }

    private void OnEnable()
    {
        gameConstants.IsPaused = false;
        gameConstants.HasFailed = false;
    }
}

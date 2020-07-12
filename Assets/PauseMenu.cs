using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField]
    Animator levelAnimator;

    [SerializeField]
    GameObject cameraCover;

    public Image PauseTextRenderer;
    public Sprite[] PauseTextImages;

    bool hasWon = false;
    private int delayCount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && gameConstants.HasFailed == false && hasWon == false)
        {
            pausePanel.SetActive(!pausePanel.activeInHierarchy);
            gameConstants.IsPaused = pausePanel.activeInHierarchy;

            if (gameConstants.IsPaused)
            {
                levelAnimator.SetFloat("pausedSpeed", 0);
                PauseTextRenderer.sprite = PauseTextImages[1];
            }
            else
            {
                levelAnimator.SetFloat("pausedSpeed", 1);
                PauseTextRenderer.sprite = PauseTextImages[0];
            }

            cameraCover.SetActive(gameConstants.IsPaused);
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
            gameConstants.GameStateChange?.Invoke(3); //change to 1 if it is different from winning the last level
        }
        else
        {
            WinPanel.SetActive(true);
            gameConstants.GameStateChange?.Invoke(3);
        }
    }

    public void FailedLevel()
    {
        //failPanel.SetActive(true);
        //gameConstants.IsPaused = true;
        //gameConstants.HasFailed = true;
        //gameConstants.GameStateChange?.Invoke(2);
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

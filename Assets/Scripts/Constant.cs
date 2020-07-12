using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Constant", menuName = "ScriptableObject/Constant")]
[System.Serializable]
public class Constant : ScriptableObject
{
    [SerializeField]
    private int targetFrameRate = 60;

    [SerializeField]
    private int frameSpeed = 60;

    [SerializeField]
    private int cooldownSpeed = 60;

    [SerializeField]
    private int postWinDelay = 60;

    [SerializeField]
    private bool isPaused = false;

    [SerializeField]
    private bool hasFailed = false;

    [SerializeField]
    private int currentLevel = 0;

    public Action<int> GameStateChange;

    public int TARGET_FRAMERATE
    {
        get => targetFrameRate;
    }

    public int FRAME_SPEED
    {
        get => frameSpeed;
    }


    public int COOLDOWN_SPEED
    {
        get => cooldownSpeed;
    }

    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public bool HasFailed { get => hasFailed; set => hasFailed = value; }
    public int PostWinDelay { get => postWinDelay;}
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
}

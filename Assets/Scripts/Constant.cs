using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

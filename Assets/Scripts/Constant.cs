using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Constant", menuName = "ScriptableObject/Constant")]
[System.Serializable]
public class Constant : ScriptableObject
{
    [SerializeField]
    private int frameSpeed = 60;

    public int FRAME_SPEED
    {
        get => frameSpeed;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObject/Game State")]
public class GameState : ScriptableObject
{
    public bool CanSelect;
    public Action TriggerCoolDown;
    public float CooldownProgress = 0;
}

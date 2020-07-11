using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameState gameState;
    public Constant gameConstant;

    // Cooldown
    private int cooldownCount = 0;
    private bool isCooldown = false;
    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = gameConstant.TARGET_FRAMERATE;
        gameState.CanSelect = true;
        gameState.TriggerCoolDown += OnTriggerCoolDown;
    }

    private void Start()
    {
        // Set the resolution to make it work in WebGL
        // Forced fullscreen
        Screen.SetResolution(960, 600, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            if (Time.frameCount - cooldownCount > gameConstant.COOLDOWN_SPEED)
            {
                gameState.CanSelect = true;
                isCooldown = false;
            }
        }
    }

    private void OnDestroy()
    {
        gameState.TriggerCoolDown -= OnTriggerCoolDown;
    }

    private void OnTriggerCoolDown()
    {
        cooldownCount = Time.frameCount;
        isCooldown = true;
        gameState.CanSelect = false;
    }
}

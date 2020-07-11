using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public int targetFramerate = 60;
    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFramerate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMHandler : MonoBehaviour
{
    public AudioClip levelBGM;
    public AudioClip winBGM;
    public AudioClip loseBGM;
    public AudioClip levelPassBGM;

    public Constant gameConstant;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameConstant.GameStateChange += OnGameStateChange;
        audioSource.clip = levelBGM;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        gameConstant.GameStateChange -= OnGameStateChange;
    }


    void OnGameStateChange(int flag)
    {
        if (flag == 1)
        {
            audioSource.clip = levelPassBGM;
        }
        else if (flag == 2) // failed
        {
            audioSource.clip = loseBGM;
        }
        else if (flag == 3)
        {
            audioSource.clip = winBGM;
        }

        audioSource.loop = true;
        audioSource.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : MonoBehaviour
{
    [SerializeField]
    public entityType.Type currentHeroType = entityType.Type.Empty;

    [SerializeField]
    EnemyHighway enemyHighwayRef;

    [SerializeField]
    Constant constantRef;

    [SerializeField]
    HeroCommandGenerator commandGenerator;

    [SerializeField]
    UnityEvent WinEvent;

    [SerializeField]
    UnityEvent LoseEvent;

    Animator heroAnimator;

    int commandIndex = 0;

    public AudioClip attackClip;
    private AudioSource audioSource;

    //Debug Only
    [SerializeField]
    IconIndicator debugIcon;

    private void Awake()
    {
        heroAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying && enemyHighwayRef.isMoving && !constantRef.IsPaused)
        {
            //resume animations
            heroAnimator.SetFloat("PausedSpeed", 1);

            if ((enemyHighwayRef.frameCount) % constantRef.FRAME_SPEED == 0)
            {
                //process next command
                if (commandIndex < commandGenerator.commandCount -1)
                {
                    commandIndex++;
                    currentHeroType = commandGenerator.commandArray[commandIndex].heroCommandType;
                    heroAnimator.SetInteger("EntityType", (int)currentHeroType);
                    debugIcon.ChangeIcon((int)currentHeroType);
                }
            }

            if ((enemyHighwayRef.frameCount - 1) % constantRef.FRAME_SPEED == 0)
            {
                CheckEnemy();
            }
        }
        else if(constantRef.IsPaused)
        {
            //pause animations
            heroAnimator.SetFloat("PausedSpeed", 0);
        }
    }

    void CheckEnemy()
    {
        Enemy e = enemyHighwayRef.GetEnemy();

        if(e == null)
        {
            //Win
            WinEvent.Invoke();
            return;
        }
        
        //Dont do anything if the space is empty
        if (e.myType == entityType.Type.Empty)
        {
            return;
        }
        else
        {
            heroAnimator.SetTrigger("Attack");
            PlayAttackSound();
        }

        switch (currentHeroType)
        {
            case entityType.Type.Rock:
                if (e.myType == entityType.Type.Sissors)
                {
                    e.Kill();
                    
                }
                else
                {
                    LoseEvent.Invoke();
                }

                break;

            case entityType.Type.Paper:
                if (e.myType == entityType.Type.Rock)
                {
                    e.Kill();
                }
                else
                {
                    LoseEvent.Invoke();
                }

                break;

            case entityType.Type.Sissors:
                if (e.myType == entityType.Type.Paper)
                {
                    e.Kill();
                }
                else
                {
                    LoseEvent.Invoke();
                }
                break;

            default:
                break;
        }
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackClip;
        audioSource.PlayDelayed(0.2f);
    }
}

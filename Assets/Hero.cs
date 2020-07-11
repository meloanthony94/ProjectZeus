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

    private void Awake()
    {
        //heroAnimator.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying && enemyHighwayRef.isMoving)
        {
            if ((enemyHighwayRef.frameCount) % constantRef.FRAME_SPEED == 0)
            {
                //process next command
                if (commandIndex < commandGenerator.commandCount)
                {
                    commandIndex++;
                    currentHeroType = commandGenerator.commandArray[commandIndex].heroCommandType;
                }
            }

            if ((enemyHighwayRef.frameCount + 1) % constantRef.FRAME_SPEED == 0)
            {
                CheckEnemy();
            }
        }
    }

    void CheckEnemy()
    {

        if(enemyHighwayRef.GetEnemy() == null)
        {
            //Win
            WinEvent.Invoke();
        }

        //Dont do anything if the space is empty
        if (enemyHighwayRef.GetEnemy().myType == entityType.Type.Empty)
        {
            return;
        }

        switch (currentHeroType)
        {
            case entityType.Type.Rock:
                if (enemyHighwayRef.GetEnemy().myType == entityType.Type.Sissors)
                {
                    print("Victory");
                    //trigger enemy death state
                }
                else
                {
                    print("Death");
                    LoseEvent.Invoke();
                }

                break;

            case entityType.Type.Paper:
                if (enemyHighwayRef.GetEnemy().myType == entityType.Type.Rock)
                {
                    print("Victory");
                    //trigger enemy death state
                }
                else
                {
                    print("Death");
                    LoseEvent.Invoke();
                }

                break;

            case entityType.Type.Sissors:
                if (enemyHighwayRef.GetEnemy().myType == entityType.Type.Sissors)
                {
                    print("Victory");
                    //trigger enemy death state
                }
                else
                {
                    print("Death");
                    LoseEvent.Invoke();
                }
                break;

            default:
                break;
        }
    }
}

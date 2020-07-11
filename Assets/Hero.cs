using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
   // public enum Type
   // {
   //     Empty, Rock, Paper, Sissors
   // }

    [SerializeField]
    public entityType.Type currentHeroType = entityType.Type.Empty;

    [SerializeField]
    EnemyHighway enemyHighwayRef;

    [SerializeField]
    Constant constantRef;

    Animator heroAnimator;

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
        if ((enemyHighwayRef.frameCount - 1) % constantRef.FRAME_SPEED == 0)
        {
            CheckEnemy();
        }
    }

    void CheckEnemy()
    {
        switch (currentHeroType)
        {
            case entityType.Type.Rock:
                if (enemyHighwayRef.GetEnemy().myType == entityType.Type.Sissors)
                {
                    print("Victory");
                }
                else
                {
                    print("Death");
                }

                break;

            case entityType.Type.Paper:
                break;
            case entityType.Type.Sissors:
                break;
            default:
                break;
        }

        //Attack
      // if(currentHeroType == enemyHighwayRef.GetEnemy().myType)
      // {
      //     print("HIIIIIII");
      // }

        //Death
    }
}

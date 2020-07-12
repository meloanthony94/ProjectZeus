using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class Enemy : MonoBehaviour
{
    //Enums
    // public enum Type
    // {
    //     Empty, Rock, Paper, Sissors
    // }


    public AudioClip deathClip;
    public AudioClip attackClip;

    private AudioSource audioSource;

    enum SelectedState
    {
        Default, Hover, Selected
    }

    [SerializeField]
    public entityType.Type myType = entityType.Type.Empty;

    [SerializeField]
    SelectedState currentSelection = SelectedState.Default;

    private SelectedState CurrentSelection
    {
        get => currentSelection;
        set
        {
            if (currentSelection != value)
            {
                currentSelection = value;
                switch (currentSelection)
                {
                    case SelectedState.Default:
                        highlightAnimator.SetTrigger("ToDefault");
                        break;
                    case SelectedState.Hover:
                        highlightAnimator.SetTrigger("ToHover");
                        break;
                    case SelectedState.Selected:
                        highlightAnimator.SetTrigger("ToSelect");
                        break;
                }
            }
        }
    }

    [SerializeField]
    GameObject[] TypeVisuals;

    Animator currentAnimator = null;
    public Animator highlightAnimator = null;

    //Flags
    [SerializeField]
    bool isHoveredFlag = false;

    [SerializeField]
    int index = -1;

    public int Index { get => index; set => index = value; }

    public EnemyHighway highway;
    public GameState gameState;

    public bool Selectable = true;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (myType != entityType.Type.Empty)
        {
            TypeVisuals[(int)entityType.Type.Empty].SetActive(false);
            TypeVisuals[(int)myType].SetActive(true);
        }

        currentAnimator = TypeVisuals[(int)myType].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       //if (Input.GetKeyDown(KeyCode.Alpha1))
       //{
       //    TypeSwap(entityType.Type.Empty);
       //}
       //
       //if (Input.GetKeyDown(KeyCode.Alpha2))
       //{
       //    TypeSwap(entityType.Type.Rock);
       //}
       //
       //if (Input.GetKeyDown(KeyCode.Alpha3))
       //{
       //    TypeSwap(entityType.Type.Paper);
       //}
       //
       //if (Input.GetKeyDown(KeyCode.Alpha4))
       //{
       //    TypeSwap(entityType.Type.Sissors);
       //}       
    }

    private void OnMouseEnter()
    {
        if (Selectable && gameState.CanSelect)
        {
            isHoveredFlag = true;
            if (CurrentSelection != SelectedState.Selected)
            {
                CurrentSelection = SelectedState.Hover;
            }
        }
    }

    private void OnMouseExit()
    {
        isHoveredFlag = false;
        //Make sure to check that it's not selected first
        if (CurrentSelection != SelectedState.Selected)
        {
            CurrentSelection = SelectedState.Default;
        }
    }

    private void OnMouseDown()
    {
        if (Selectable && gameState.CanSelect)
        {
            //check for cooldown
            //report my index
            CurrentSelection = SelectedState.Selected;

            // if select is a valid move
            highway.Register(this);
        }
    }

    public void TypeSwap(entityType.Type newType)
    {
        myType = newType;

        for (int i = 0; i < TypeVisuals.Length; i++)
        {
            TypeVisuals[i].SetActive(false);
        }

        TypeVisuals[(int)newType].SetActive(true);

        currentAnimator = TypeVisuals[(int)myType].GetComponent<Animator>();
        CurrentSelection = SelectedState.Default;
    }

    public void Lock()
    {
        Selectable = false;
    }

    public void Attack()
    {
        currentAnimator.SetTrigger("Attack");
        PlayAttackSound();
    }

    public void Kill()
    {
        currentAnimator.SetTrigger("AttackReflection");
        PlayDeathSound();
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackClip;
        audioSource.PlayDelayed(0.2f);
    }

    public void PlayDeathSound()
    {
        audioSource.clip = deathClip;
        audioSource.PlayDelayed(0.5f);
    }
}

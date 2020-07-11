using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy : MonoBehaviour
{
    //Enums
  // public enum Type
  // {
  //     Empty, Rock, Paper, Sissors
  // }

    enum SelectedState
    {
        Default, Hover, Selected
    }

    [SerializeField]
    public entityType.Type myType = entityType.Type.Empty;

    [SerializeField]
    SelectedState currentSelection = SelectedState.Default;

    [SerializeField]
    GameObject[] TypeVisuals;

    Animator currentAnimator = null;

    //Flags
    [SerializeField]
    bool isHoveredFlag = false;

    //
    int index = -1;

    public int Index { get => index; set => index = value; }

    public EnemyHighway highway;
    public bool selected;



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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TypeSwap(entityType.Type.Empty);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TypeSwap(entityType.Type.Rock);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TypeSwap(entityType.Type.Paper);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TypeSwap(entityType.Type.Sissors);
        }
    }

    private void OnMouseEnter()
    {
        isHoveredFlag = true;
        currentSelection = SelectedState.Hover;
    }

    private void OnMouseExit()
    {
        isHoveredFlag = false;
        //Make sure to check that it's not selected first
        currentSelection = SelectedState.Default;
    }

    private void OnMouseDown()
    {
        //check for cooldown
        //report my index
        currentSelection = SelectedState.Selected;

        // if select is a valid move
        highway.Register(this);
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
    }
}

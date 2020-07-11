using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy : MonoBehaviour
{
    //Enums
    public enum Type
    {
        Empty, Rock, Paper, Sissors
    }

    enum SelectedState
    {
        Default, Hover, Selected
    }

    [SerializeField]
    public Type myType = Type.Empty;

    [SerializeField]
    SelectedState currentSelection = SelectedState.Default;

    [SerializeField]
    GameObject[] TypeVisuals; 

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TypeSwap(Type.Empty);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TypeSwap(Type.Rock);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TypeSwap(Type.Paper);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TypeSwap(Type.Sissors);
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
        print("CLICKED");
        //check for cooldown
        //report my index
        currentSelection = SelectedState.Selected;

        // if select is a valid move
        highway.Register(this);
    }

    public void TypeSwap(Type newType)
    {
        myType = newType;

        //for (int i = 0; i < TypeVisuals.Length; i++)
        //{
        //    TypeVisuals[i].SetActive(false);
        //}

        //TypeVisuals[(int)newType].SetActive(true);
    }
}

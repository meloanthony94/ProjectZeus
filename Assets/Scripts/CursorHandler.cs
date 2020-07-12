using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    public Texture2D defaultPointer;
    public Texture2D clickPointer;
    public Texture2D waitPointer;
    public Texture2D[] waitPointers;

    private bool clicked = false;

    public GameState gameState;

    public AudioClip errorClip;
    private AudioSource audio;

    private enum CursorState
    {
        Default,
        Click,
        Wait
    }

    private CursorState currentState = CursorState.Default;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultPointer, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {

        CursorState newState = Input.GetMouseButton(0) ? CursorState.Click : CursorState.Default;
        
        if (!gameState.CanSelect)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    audio.PlayOneShot(errorClip);
            //}
            newState = CursorState.Wait;
        }



        if (newState != currentState)
        {
            switch (newState)
            {
                case CursorState.Default:
                    Cursor.SetCursor(defaultPointer, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                case CursorState.Click:
                    Cursor.SetCursor(clickPointer, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                case CursorState.Wait:
                    Cursor.SetCursor(waitPointer, Vector2.zero, CursorMode.ForceSoftware);
                    break;
            }

            currentState = newState;
        }
        else if (currentState == CursorState.Wait)
        {
            int textureIndex = (int)(gameState.CooldownProgress * (waitPointers.Length - 1));

            Cursor.SetCursor(waitPointers[textureIndex], Vector2.zero, CursorMode.ForceSoftware);
        }



    }
}

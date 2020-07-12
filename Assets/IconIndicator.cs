using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconIndicator : MonoBehaviour
{
    [SerializeField]
    Sprite[] Icons;

    SpriteRenderer myrenderer;

    // Start is called before the first frame update
    void Start()
    {
        myrenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeIcon(int index)
    {
        myrenderer.sprite = Icons[index];
    }
}

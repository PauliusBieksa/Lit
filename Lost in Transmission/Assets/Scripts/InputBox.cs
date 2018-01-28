using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBox : MonoBehaviour
{

    SpriteRenderer dir;
    Dirs dEnum = Dirs.NONE;
    SpriteRenderer input;
    Button bEnum = Button.NONE;

    SpriteLibrary sL;

    // Use this for initialization
    void Start()
    {
        sL = FindObjectOfType<SpriteLibrary>();

        SpriteRenderer[] rens = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < rens.Length; ++i)
        {
            if (rens[i].gameObject.name == "direction")
            {
                dir = rens[i];
            }
            else if (rens[i].gameObject.name == "input")
            {
                input = rens[i];
            }
        }
        Button = bEnum;
    }

    public Button Button
    {
        get
        {
            return bEnum;
        }
        set
        {
            input.sprite = sL.GettButt(value);
            bEnum = value;
        }
    }

    public Dirs Direction
    {
        get
        {
            return dEnum;
        }
        set
        {
            dir.sprite = sL.GetD(value);
            dEnum = value;
        }
    }
}

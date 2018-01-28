using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLibrary : MonoBehaviour {

     public Sprite[] locks = new Sprite[3];
     public Sprite arrow;
     public Sprite[] butts = new Sprite[9];
     public Sprite[] cards = new Sprite[5];
     public Sprite[] dpads = new Sprite[9];

    public Sprite GetLock(Locks l)
    {
        return locks[(int)l];
    }

    public Sprite GettButt(Button b)
    {
        if (b != Button.NONE)
        {
            return butts[(int)b];
        }
        else
        {
            return butts[butts.Length - 1];
        }
    }

    public Sprite GetCard(Button b)
    {
        return cards[(int)b];
    }

    public Sprite GetD(Dirs d)
    {
        switch (d)
        {
            case Dirs.N:
                return dpads[1];
            case Dirs.NE:
                return dpads[2];
            case Dirs.E:
                return dpads[3];
            case Dirs.SE:
                return dpads[4];
            case Dirs.S:
                return dpads[5];
            case Dirs.SW:
                return dpads[6];
            case Dirs.W:
                return dpads[7];
            case Dirs.NW:
                return dpads[8];
            default:
                return dpads[0];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Locks
{
    OPEN, CLOSED, HECKA
};

public class SpriteLibrary : MonoBehaviour {

     public Sprite[] locks = new Sprite[3];
     public Sprite arrow;
     public Sprite[] butts = new Sprite[6];
     public Sprite[] cards = new Sprite[5];
     public Sprite[] dpads = new Sprite[9];

    public Sprite GetLock(Locks l)
    {
        return locks[(int)l];
    }

    public Sprite GettButt(Button b)
    {
        return butts[(int)b];
    }

    public Sprite GetCard(Button b)
    {
        return cards[(int)b];
    }

    public Sprite GetD(Dirs d)
    {
        return dpads[(int)d];
    }
}

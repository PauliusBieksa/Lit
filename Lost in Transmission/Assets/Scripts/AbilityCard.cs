using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : MonoBehaviour
{
    int cooldown = 0;
    Locks locked = Locks.OPEN;

    SpriteRenderer locker;

    SpriteLibrary sL;

    // Use this for initialization
    void Start()
    {
        SpriteRenderer[] rends = GetComponentsInChildren<SpriteRenderer>();
        locker = GetComponentInChildren<SpriteRenderer>();
        for (int i = 0; i < rends.Length; ++i)
        {
            if (rends[i].name == "abiLock")
            {
                locker = rends[i];
                break;
            }
        }
        sL = FindObjectOfType<SpriteLibrary>();
    }

    public int Cooldown
    {
        get
        {
            return cooldown;
        }
        set
        {
            // Add the number here
        }
    }

    public Locks Locked
    {
        get
        {
            return locked;
        }
        set
        {
            locked = value;
            locker.sprite = sL.GetLock(value);
        }
    }
}

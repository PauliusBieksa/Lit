using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueInput : MonoBehaviour
{
    [SerializeField]
    PlayerInput pI;
    [SerializeField]
    Turn_manager_script tms;

    int heldIndex;
    Dirs dir = Dirs.NONE;
    bool[] held = new bool[4];
    Button[] butts = new Button[4];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            held[i] = false;
        }
        butts[0] = Button.A;
        butts[1] = Button.B;
        butts[2] = Button.X;
        butts[3] = Button.Y;
    }

    // Update is called once per frame
    void Update()
    {
        dir = pI.CompassInput();
        for (int i = 0; i < 4; ++i)
        {
            if (!held[i] && pI.ButtonHeld(butts[i]))
            {
                held[i] = true;
                heldIndex = i;
            }
            else if (held[i] && !pI.ButtonHeld(butts[i]))
            {
                held[i] = false;
                if (i == heldIndex && tms.Cooldown(buttToMove(butts[i])) && dir != Dirs.NONE)
                {
                    Move m = new Move();
                    m.dir = dir;
                    m.type = buttToMove(butts[i]);
                    // Send move to paulius
                    Debug.Log("QI " + m.dir.ToString() + " " + m.type.ToString());
                    tms.AddMove(m);
                }
            }
        }

        if (pI.LTButtonDown)
        {
            tms.QueueUp();
        }
        else if (pI.RTButtonDown)
        {
            tms.QueueDown();
        }
    }


            MoveTypes buttToMove(Button butt)
    {
        switch (butt)
        {
            case Button.A:
                return MoveTypes.MOVE;
            case Button.B:
                return MoveTypes.MELEE;
            case Button.X:
                return MoveTypes.BLOCK;
            case Button.Y:
                return MoveTypes.RANGE;
            default:
                return MoveTypes.MOVE;
        }
    }
}

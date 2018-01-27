using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueInput : MonoBehaviour
{
    [SerializeField]
    GameObject player;
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
        pI = player.GetComponent<PlayerInput>();
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
                if (i == heldIndex && tms.Cooldown(buttToMove(butts[i])) && dir != Dirs.NONE /*and direction indicated*/)
                {
                    Move m = new Move();
                    // Send move to paulius
                    tms.AddMove(m);
                }
            }
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

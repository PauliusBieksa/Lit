using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueInput : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    PlayerInput pI;
    int heldIndex;
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
        for (int i = 0; i < 4; ++i)
        {
            if (!held[i] && pI.ButtonHeld(butts[i]))
            {
                held[i] = true;
                heldIndex = i;
            }
            else if(held[i] && !pI.ButtonHeld(butts[i]))
            {
                held[i] = false;
                if(i == heldIndex /*and direction indicated*/)
                {
                    // Send move to paulius
                }
            }
        }
    }
}

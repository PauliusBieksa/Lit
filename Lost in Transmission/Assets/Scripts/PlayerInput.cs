using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private int controllerNumber = -1;
    private string horizontalAxis;
    private string verticalAxis;
    private string aButt;
    private string bButt;
    private string xButt;
    private string yButt;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private float divider = Mathf.Cos(22.5f);


    public void setControllerNumber(int conNum)
    {
        controllerNumber = conNum;
        horizontalAxis = controllerNumber + "_horizontal";
        verticalAxis = controllerNumber + "_vertical";
        aButt = controllerNumber + "_A";
        bButt = controllerNumber + "_B";
        xButt = controllerNumber + "_X";
        yButt = controllerNumber + "_Y";
    }

    public bool ButtonDown(Button butt)
    {
        switch (butt)
        {
            case Button.A:
                return Input.GetButtonDown(aButt);
            case Button.B:
                return Input.GetButtonDown(bButt);
            case Button.X:
                return Input.GetButtonDown(xButt);
            case Button.Y:
                return Input.GetButtonDown(yButt);
        }
        return false;
    }

    private void FixedUpdate()
    {
        Horizontal = Input.GetAxisRaw(horizontalAxis);
        Vertical = Input.GetAxisRaw(verticalAxis);
    }

    public Vector3 JoystickInput()
    {
        return new Vector3(Horizontal, 0.0f, Vertical).normalized;
    }

    public Dirs CompassInput()
    {
        float yDir = Vector3.Cross(new Vector3(0.0f, 0.0f, 1.0f), JoystickInput()).y;
        float dot = Vector3.Dot(new Vector3(0.0f, 0.0f, 1.0f), JoystickInput());

        if (dot > (1 - divider))
        {
            return Dirs.N;
        }
        else if (dot <= (-1 + divider))
        {
            return Dirs.S;
        }

        if (yDir > 0)
        {
            if (dot > divider)
            {
                return Dirs.NW;
            }
            else if (dot > (0 - divider))
            {
                return Dirs.W;
            }
            else if (dot > (-1 + divider))
            {
                return Dirs.SW;
            }
        }
        else if (yDir < 0)
        {
            if (dot > divider)
            {
                return Dirs.NE;
            }
            else if (dot > (0 - divider))
            {
                return Dirs.E;
            }
            else if (dot > (-1 + divider))
            {
                return Dirs.SE;
            }
        }
        return Dirs.N;
    }
}

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
    float divider = 1.0f - Mathf.Cos((Mathf.PI * 22.5f) / 180.0f);

    public float dotOut;
    public float zOut;

    public bool hasController()
    {
        return controllerNumber > 0;
    }

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
        Vector3 outVec = new Vector3(Horizontal, Vertical, 0.0f);
        return Vector3.Normalize(outVec);
    }

    public Dirs CompassInput()
    {
        float zDir = Vector3.Cross(new Vector3(0.0f, 1.0f, 0.0f), JoystickInput()).z;
        float dot = Vector3.Dot(new Vector3(0.0f, 1.0f, 0.0f), JoystickInput());

        dotOut =  dot;
        zOut = zDir;

        if (dot > (1.0f - divider))
        {
            return Dirs.N;
        }
        else if (dot <= ((-1.0f) + divider))
        {
            return Dirs.S;
        }

        if (zDir > 0.0f)
        {
            if (dot > divider)
            {
                return Dirs.NW;
            }
            else if (dot > (0.0f - divider))
            {
                return Dirs.W;
            }
            else if (dot > ((-1.0f) + divider))
            {
                return Dirs.SW;
            }
        }
        else if (zDir < 0.0f)
        {
            if (dot > divider)
            {
                return Dirs.NE;
            }
            else if (dot > (0.0f - divider))
            {
                return Dirs.E;
            }
            else if (dot > (-1.0f + divider))
            {
                return Dirs.SE;
            }
        }
        return Dirs.N;
    }
}

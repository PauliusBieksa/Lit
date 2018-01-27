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
    private string lTrigg;
    private string rTrigg;
    private string lBump;
    private string rBump;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    private float divider = 1.0f - Mathf.Cos((Mathf.PI * 22.5f) / 180.0f);

    private float lTriggered;
    private bool lRead;
    private bool lHeld;
    private float rTriggered;
    private bool rRead;
    private bool rHeld;

    private float dotOut;
    private float zOut;

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
        lTrigg = controllerNumber + "_LTrigger";
        rTrigg = controllerNumber + "_RTrigger";
        lBump = controllerNumber + "_LBumper";
        rBump = controllerNumber + "_RBumper";
    }

    public bool LTButtonDown
    {
        get
        {
            if (lRead)
            {
                if (lTriggered > 0.0f)
                {
                    lRead = true;
                }
            }
            return lRead;
        }
    }

    public bool LTDown
    {
        get
        {
            return lHeld;
        }
    }

    public bool RTButtonDown
    {
        get
        {
            if (rRead)
            {
                if (rTriggered > 0.0f)
                {
                    rRead = true;
                }
            }
            return rRead;
        }
    }

    public bool RTDown
    {
        get
        {
            return rHeld;
        }
    }

    public bool ButtonDown(Button butt)
    {
        if (!hasController())
        {
            return false;
        }
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
            case Button.LB:
                return Input.GetButtonDown(lBump);
            case Button.RB:
                return Input.GetButtonDown(rBump);
        }
        return false;
    }

    public bool ButtonHeld(Button butt)
    {
        if (!hasController())
        {
            return false;
        }
        switch (butt)
        {
            case Button.A:
                return Input.GetButton(aButt);
            case Button.B:
                return Input.GetButton(bButt);
            case Button.X:
                return Input.GetButton(xButt);
            case Button.Y:
                return Input.GetButton(yButt);
            case Button.LB:
                return Input.GetButton(lBump);
            case Button.RB:
                return Input.GetButton(rBump);
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (hasController())
        {
            Horizontal = Input.GetAxisRaw(horizontalAxis);
            Vertical = Input.GetAxisRaw(verticalAxis);
            float trigger = Input.GetAxisRaw(lTrigg);
            if (trigger != 0.0f)
            {
                if (!lHeld)
                {
                    lTriggered = trigger;
                    lHeld = true;
                }
            }
            else
            {
                lTriggered = trigger;
                lHeld = false;
                lRead = false;
            }
            trigger = Input.GetAxisRaw(rTrigg);
            if (trigger != 0.0f)
            {
                if (!rHeld)
                {
                    rTriggered = trigger;
                    rHeld = true;
                }
            }
            else
            {
                rTriggered = trigger;
                rHeld = false;
                rRead = false;
            }
        }
    }

    public Vector3 JoystickInput()
    {
        if (!hasController())
        {
            return new Vector3();
        }
        Vector3 outVec = new Vector3(Horizontal, Vertical, 0.0f);
        return Vector3.Normalize(outVec);
    }

    public Dirs CompassInput()
    {
        if (!hasController())
        {
            return Dirs.N;
        }
        float zDir = Vector3.Cross(new Vector3(0.0f, 1.0f, 0.0f), JoystickInput()).z;
        float dot = Vector3.Dot(new Vector3(0.0f, 1.0f, 0.0f), JoystickInput());

        dotOut = dot;
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

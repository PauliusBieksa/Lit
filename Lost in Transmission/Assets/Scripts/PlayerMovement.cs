using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerInput pI;

    public Dirs outCompass;
    public int outAngle;
    public Vector3 outVec;

    // Use this for initialization
    void Start()
    {
        pI = GetComponent<PlayerInput>();
        pI.setControllerNumber(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pI.JoystickInput() != new Vector3())
        {
            outCompass = pI.CompassInput();
            outAngle = (int)outCompass;
            outVec = pI.JoystickInput();
            transform.Translate(Quaternion.Euler(0.0f, 0.0f, 360 - (int)pI.CompassInput()) * new Vector3(0.0f, 1.0f, 0.0f) * Time.fixedDeltaTime);
        }
        if (pI.ButtonDown(Button.A))
        {
            transform.Translate(new Vector3(0.0f, -1.0f, 0.0f));
        }
        if (pI.ButtonDown(Button.Y))
        {
            transform.Translate(new Vector3(0.0f, 1.0f, 0.0f));
        }
        if (pI.ButtonDown(Button.B))
        {
            transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
        }
        if (pI.ButtonDown(Button.X))
        {
            transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f));
        }
        //transform.Translate(pI.JoystickInput() * Time.fixedDeltaTime * 10.0f);
    }
}

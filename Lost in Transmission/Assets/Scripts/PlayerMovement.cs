using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerInput pI;
    [SerializeField]
    Turn_manager_script tms;

    public Dirs outCompass;
    public int outAngle;
    public Vector3 outVec;

    // Use this for initialization
    void Start()
    {
        pI = GetComponent<PlayerInput>();
        //pI.setControllerNumber(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pI.JoystickInput() != new Vector3())
        {
            outCompass = pI.CompassInput();
            outAngle = (int)outCompass;
            outVec = pI.JoystickInput();
            //transform.Translate(Quaternion.Euler(0.0f, 0.0f, (int)pI.CompassInput()) * new Vector3(0.0f, 1.0f, 0.0f) * Time.fixedDeltaTime);
        }
        if (pI.LTButtonDown)
        {
            transform.Translate(new Vector3(0.0f, -1.0f, 0.0f));
        }
        if (pI.RTButtonDown)
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

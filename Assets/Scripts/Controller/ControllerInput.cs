using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Comparison
{
    Pressed,
    Released
};

public class ControllerInput{

    public static float GetLeftAnalogStickXValue()
    {
        return Input.GetAxis("Walk");
    }

    public static bool GetBButton()
    {
        return Input.GetButton("BButton");
    }
    
    public static bool GetRightBumperDown()
    {
        return Input.GetButtonDown("RightBumper");
    }
}

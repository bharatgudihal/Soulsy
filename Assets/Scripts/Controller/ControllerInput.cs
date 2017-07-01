using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput{

    public static float GetLeftAnalogStickXValue()
    {
        return Input.GetAxis("Walk");
    }

    public static bool GetBButtonValue()
    {
        return Input.GetButton("BButton");
    }
    
     
}

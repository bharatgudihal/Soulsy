using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeftJoystick : MonoBehaviour{

    [Range(0,1)]
    public  float threshold;

    public enum Comparison
    {
        Equal,
        NotEqual,
        Greater,
        GreaterEqual,
        Lower,
        LowerEqual
    };

    public Comparison type = Comparison.Equal;

    private bool LeftJoystickXaxisValue()
    {
        if(type == Comparison.Equal)
            return ControllerInput.GetLeftAnalogStickXValue() == threshold;

        if (type == Comparison.NotEqual)
            return ControllerInput.GetLeftAnalogStickXValue() != threshold;

        if(type == Comparison.Greater)
            return ControllerInput.GetLeftAnalogStickXValue() > threshold;

        if (type == Comparison.GreaterEqual)
            return ControllerInput.GetLeftAnalogStickXValue() >= threshold;

        if (type == Comparison.Lower)
            return ControllerInput.GetLeftAnalogStickXValue() < threshold;

        if (type == Comparison.LowerEqual)
            return ControllerInput.GetLeftAnalogStickXValue() <= threshold;

        else
            return true;
    }
}

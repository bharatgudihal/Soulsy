using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeftJoystick : MonoBehaviour{

    [Range(0,1)]
    public  float walkValue;

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
            return ControllerInput.GetLeftAnalogStickXValue() == walkValue;

        if (type == Comparison.NotEqual)
            return ControllerInput.GetLeftAnalogStickXValue() != walkValue;

        if(type == Comparison.Greater)
            return ControllerInput.GetLeftAnalogStickXValue() > walkValue;

        if (type == Comparison.GreaterEqual)
            return ControllerInput.GetLeftAnalogStickXValue() >= walkValue;

        if (type == Comparison.Lower)
            return ControllerInput.GetLeftAnalogStickXValue() < walkValue;

        if (type == Comparison.LowerEqual)
            return ControllerInput.GetLeftAnalogStickXValue() <= walkValue;

        else
            return true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CheckLeftJoystick : Condition{

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
            return Mathf.Abs(ControllerInput.GetLeftAnalogStickXValue()) == threshold;

        if (type == Comparison.NotEqual)
            return Mathf.Abs(ControllerInput.GetLeftAnalogStickXValue()) != threshold;

        if(type == Comparison.Greater)
            return Mathf.Abs(ControllerInput.GetLeftAnalogStickXValue()) > threshold;

        if (type == Comparison.GreaterEqual)
            return Mathf.Abs(ControllerInput.GetLeftAnalogStickXValue()) >= threshold;

        if (type == Comparison.Lower)
            return Mathf.Abs(ControllerInput.GetLeftAnalogStickXValue()) < threshold;

        if (type == Comparison.LowerEqual)
            return Mathf.Abs(ControllerInput.GetLeftAnalogStickXValue()) <= threshold;

        else
            return false;
    }

    public override bool Check(GameObject gameObject, GameObject other, List<Effect> effects, Stats stats)
    {
        return LeftJoystickXaxisValue();
    }
}

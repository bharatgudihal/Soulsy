using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBButton : MonoBehaviour {

    public enum Comparison
    {
        Pressed,
        Released
    };

    public Comparison type = Comparison.Pressed;

    private bool BButtonValue()
    {
        if (type == Comparison.Pressed)
            return ControllerInput.GetBButtonValue() == true;

        if (type == Comparison.Released)
            return ControllerInput.GetBButtonValue() == false;

        else
            return true;
    }
}

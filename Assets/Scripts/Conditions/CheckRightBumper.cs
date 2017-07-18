using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CheckRightBumper : Condition
{
    public Comparison type = Comparison.Pressed;

    private bool RightBumperValue()
    {
        if (type == Comparison.Pressed)
            return ControllerInput.GetRightBumperDown() == true;

        if (type == Comparison.Released)
            return ControllerInput.GetRightBumperDown() == false;

        else
            return false;
    }

    public override bool Check(GameObject gameObject, GameObject other, List<Effect> effects, Stats stats)
    {
        return RightBumperValue();
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CheckBButton : Condition {    

    public Comparison type = Comparison.Pressed;

    private bool BButtonValue()
    {
        if (type == Comparison.Pressed)
            return ControllerInput.GetBButton() == true;

        if (type == Comparison.Released)
            return ControllerInput.GetBButton() == false;

        else
            return false;
    }

    public override bool Check(GameObject gameObject, GameObject other, List<Effect> effects, Stats stats)
    {
        return BButtonValue();
    }
}

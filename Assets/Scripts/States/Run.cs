using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State {

    Vector2 runVect;

    // Use this for initialization
    void Start()
    {
        stats = GetComponent<Stats>();
        runVect = new Vector2(GetComponent<Stats>().runSpeed, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Condition condition = CheckConditions();
        if (condition != null)
        {
            SwitchState(condition.transitionTo);
        }
        float direction = Mathf.Sign(ControllerInput.GetLeftAnalogStickXValue());
        transform.Translate(runVect * direction);

    }
}

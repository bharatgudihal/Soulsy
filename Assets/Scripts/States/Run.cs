using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State {

    Vector2 runVect;

    // Use this for initialization
    void Start()
    {
        runVect = new Vector2(GetComponent<Stats>().runSpeed, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckConditions();
        float direction = Mathf.Sign(ControllerInput.GetLeftAnalogStickXValue());
        transform.Translate(runVect * direction);

    }
}

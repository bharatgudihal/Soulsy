using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State {

    private Vector2 walkVector;

    // Use this for initialization
    void Start() {
        walkVector = new Vector2(GetComponent<Stats>().walkSpeed, 0f);
    }

    // Update is called once per frame
    void Update() {
        CheckConditions();
        float value = ControllerInput.GetLeftAnalogStickXValue();
        transform.Translate(walkVector * value);
    }
}

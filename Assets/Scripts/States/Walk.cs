using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State {

    [SerializeField]
    private float walkingSpeed;

    private Vector2 walkVector;

    // Use this for initialization
    void Start() {
        walkVector = new Vector2(walkingSpeed, 0f);
    }

    // Update is called once per frame
    void Update() {
        CheckConditions();
        float value = ControllerInput.GetLeftAnalogStickXValue();
        transform.Translate(walkVector * value);
    }
}

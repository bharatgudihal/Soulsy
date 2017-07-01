using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State {

    [SerializeField]
    private float walkingSpeed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        CheckConditions();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : State {

   

    // Use this for initialization
    void Start() {
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update() {

        TransitionUnit condition = CheckConditions();
        if (condition != null)
        {
            SwitchState(condition.state);
        }
        else
        {
            transform.position += transform.right * stats.walkSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State {

    private bool firstEntry = true;

    // Update is called once per frame
    void Update () {
        TransitionUnit condition = CheckConditions();
        if (!firstEntry && condition != null)
        {
            firstEntry = true;
            SwitchState(condition.state);
        }
        else if(firstEntry)
        {
            firstEntry = false;
            Debug.Log("Attack");
            if (animator)
            {
                animator.Play("Attack");
            }
        }
    }
}

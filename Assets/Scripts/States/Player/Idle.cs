using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{

	// Use this for initialization
	void Start () {
        stats = GetComponent<Stats>(); 
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Idle");
        TransitionUnit condition = CheckConditions();
        if (condition != null)
        {
            SwitchState(condition.state);
        }
        else
        {
            animator.Play("Idle");
        }
	}
}

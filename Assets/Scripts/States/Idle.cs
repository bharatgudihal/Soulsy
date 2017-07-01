using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Condition condition = CheckConditions();
        if (condition)
        {
            gameObject.AddComponent(condition.transitionTo);
            Destroy(this);
        }
	}
}

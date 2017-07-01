using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour {
    
    public Type transitionTo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract bool Check(GameObject gameObject, GameObject other, List<Effect> effects, Stats stats);
}

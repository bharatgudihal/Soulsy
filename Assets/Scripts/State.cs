using System.Collections.Generic;
using UnityEngine;
using System;



public class State : MonoBehaviour {

    [SerializeField]
    public List<TransitionUnit> transitions;
    
    [SerializeField]
    private List<Effect> effects;

    [SerializeField]
    private float overLapSphereRadius;

    protected Stats stats;

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<Stats>();
    }

    protected TransitionUnit CheckConditions(GameObject other = null)
    {
        TransitionUnit triggerCondition = null;
        foreach (TransitionUnit updateCondition in transitions)
        {
            if(updateCondition.condition.Check(gameObject, other, effects, stats) ^ updateCondition.checkNegative)
            {
                triggerCondition = updateCondition;
                break;
            }
        }
        return triggerCondition;
    }

    protected void SwitchState(State state)
    {
        state.enabled = true;
        enabled = false;
    }

    public void Optimize()
    {
        TransitionUnit[] optimizedList = new TransitionUnit[transitions.Count];
        foreach(TransitionUnit unit in transitions)
        {
            optimizedList[unit.priority] = unit;
        }
        transitions.Clear();
        transitions.AddRange(optimizedList);
    }
}

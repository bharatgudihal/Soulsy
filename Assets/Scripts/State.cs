using System.Collections.Generic;
using UnityEngine;
using System;



public class State:MonoBehaviour {

    [SerializeField]
    public List<TransitionUnit> UpdateConditions;

    [SerializeField]
    private List<TransitionUnit> OverLapConditions;
    
    [SerializeField]
    private List<Effect> effects;

    [SerializeField]
    private float overLapSphereRadius;

    protected Stats stats;

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected TransitionUnit CheckConditions(GameObject other = null)
    {
        TransitionUnit triggerCondition = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, overLapSphereRadius);
        if(colliders.Length > 0)
        {
            foreach (TransitionUnit overlapCondition in OverLapConditions)
            {
                foreach (Collider collider in colliders)
                {
                    if(overlapCondition.condition.Check(gameObject, collider.gameObject, effects, stats) ^ overlapCondition.checkNegative)
                    {
                        triggerCondition = overlapCondition;
                        break;
                    }                    
                }
                if (triggerCondition != null)
                {
                    break;
                }
            }
        }
        if (triggerCondition == null)
        {
            foreach(TransitionUnit updateCondition in UpdateConditions)
            {
                if(updateCondition.condition.Check(gameObject, other, effects, stats) ^ updateCondition.checkNegative)
                {
                    triggerCondition = updateCondition;
                    break;
                }
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
        TransitionUnit[] optimizedList = new TransitionUnit[UpdateConditions.Count];
        foreach(TransitionUnit unit in UpdateConditions)
        {
            optimizedList[unit.priority] = unit;
        }
        UpdateConditions.Clear();
        UpdateConditions.AddRange(optimizedList);
        optimizedList = new TransitionUnit[OverLapConditions.Count];
        foreach (TransitionUnit unit in OverLapConditions)
        {
            optimizedList[unit.priority] = unit;
        }
        OverLapConditions.Clear();
        OverLapConditions.AddRange(optimizedList);
    }
}

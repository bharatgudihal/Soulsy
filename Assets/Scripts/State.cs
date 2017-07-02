using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TransitionUnit
{
    public Condition condition;
    public State state;
}

public class State:MonoBehaviour {

    [SerializeField]
    private List<TransitionUnit> UpdateConditions;

    [SerializeField]
    private List<TransitionUnit> OverLapConditions;
    
    [SerializeField]
    private List<Effect> effects;

    [SerializeField]
    private float overLapSphereRadius;

    protected Stats stats;



    protected TransitionUnit CheckConditions()
    {
        TransitionUnit triggerCondition = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, overLapSphereRadius);
        if(colliders.Length > 0)
        {
            foreach (TransitionUnit overlapCondition in OverLapConditions)
            {
                foreach (Collider collider in colliders)
                {
                    if(overlapCondition.condition.Check(gameObject, collider.gameObject, effects, stats))
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
                if(updateCondition.condition.Check(gameObject, null, effects, stats))
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
}

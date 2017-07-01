using System.Collections.Generic;
using UnityEngine;

public class State:MonoBehaviour {

    [SerializeField]
    private List<Condition> UpdateConditions;

    [SerializeField]
    private List<Condition> OverLapConditions;
    
    [SerializeField]
    private List<Effect> effects;

    [SerializeField]
    private float overLapSphereRadius;

    [SerializeField]
    private Stats stats;

    protected Condition CheckConditions()
    {
        Condition triggerCondition = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, overLapSphereRadius);
        if(colliders.Length > 0)
        {
            foreach (Condition overlapCondition in OverLapConditions)
            {
                foreach (Collider collider in colliders)
                {
                    if(overlapCondition.Check(gameObject, collider.gameObject, effects, stats))
                    {
                        triggerCondition = overlapCondition;
                        break;
                    }                    
                }
                if (triggerCondition)
                {
                    break;
                }
            }
        }
        if (!triggerCondition)
        {
            foreach(Condition updateCondition in UpdateConditions)
            {
                if(updateCondition.Check(gameObject, null, effects, stats))
                {
                    triggerCondition = updateCondition;
                    break;
                }
            }
        }
        return triggerCondition;
    }

}

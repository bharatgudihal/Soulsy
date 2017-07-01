using System.Collections.Generic;
using UnityEngine;

public class State:MonoBehaviour {

    [SerializeField]
    private List<Condition> UpdateConditions;

    [SerializeField]
    private List<Condition> OverLapConditions;

    [SerializeField]
    private float overLapSphereRadius;

    [SerializeField]
    private List<Effect> effects;

    [SerializeField]
    private Stats stats;
   
}

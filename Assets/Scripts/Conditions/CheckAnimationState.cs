using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CheckAnimationState : Condition
{
    [Range(0,100)]
    public float percentageCompletion;

    public override bool Check(GameObject gameObject, GameObject other, List<Effect> effects, Stats stats)
    {
        return gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > percentageCompletion / 100f;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State {

    [SerializeField]
    private BoxCollider2D collider;

    private bool firstEntry = true;
    
    // Update is called once per frame
    void Update () {
        TransitionUnit condition = CheckConditions();
        if (!firstEntry && condition != null)
        {
            firstEntry = true;
            collider.enabled = false;
            SwitchState(condition.state);
        }
        else if(firstEntry)
        {
            firstEntry = false;
            if (animator)
            {
                animator.Play("Attack");
            }
        }
        else
        {
            DoColliderStuff();
        }
    }

    private void DoColliderStuff()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.normalizedTime >= stats.weapon.weaponAttackWindowStart && !collider.enabled)
        {
            collider.offset = stats.weapon.colliderOffset;
            collider.size = stats.weapon.colliderSize;
            collider.enabled = true;
        }
        if(stateInfo.normalizedTime >= stats.weapon.weaponAttackWindowEnd && collider.enabled)
        {
            collider.enabled = false;
        }
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other == collider)
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider == collider)
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CheckDistanceFromPlayer : Condition {

    [SerializeField]
    float attackDistance;

    private bool PlayerInRange(GameObject enemy, GameObject player)
    {

        player = GameObject.Find("Player");

        if (player != null)
        { 

            if (Vector2.Distance(player.transform.position, enemy.transform.position) > attackDistance)
            {
                
                return false;
            }

            else
                return true;
        }
        return false;
    }


    public override bool Check(GameObject gameObject, GameObject other, List<Effect> effects, Stats stats)
    {
        return PlayerInRange(gameObject,other);
    }
}

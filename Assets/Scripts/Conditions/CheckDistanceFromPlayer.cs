using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CheckDistanceFromPlayer : Condition {

    [SerializeField]
    float distance = 5.0f;

    private bool PlayerInRange(GameObject enemy, GameObject player)
    {
 
        if (Vector2.Distance(player.transform.position, enemy.transform.position) > 1.0f)
        {
            enemy.transform.Translate(1.0f,0.0f,0.0f);
            return false;
        }

        else
            return true;
    }


    public override bool Check(GameObject gameObject, GameObject other, List<Effect> effects, Stats stats)
    {
        return PlayerInRange(gameObject,other);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CheckPlayerWithinView : Condition
{

    [SerializeField]
    float distance = 5.0f;


    private bool PlayerInView(GameObject gameObject)
    {

        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.left,distance);
        if (hit.collider != null && hit.collider.gameObject.name == "Player")
        {
            return true;
        }

        else
            return false;
    }


    public override bool Check(GameObject gameObject, GameObject other, List<Effect> effects, Stats stats)
    {
        return PlayerInView(gameObject);
    }
}

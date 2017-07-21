using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CheckPlayerWithinView : Condition
{

    [SerializeField]
    float distance;

    LayerMask layerMask;

    private void OnEnable()
    {
        layerMask = 1 << LayerMask.NameToLayer("Player");
    }

    private bool PlayerInView(GameObject gameObject)
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right, distance,layerMask);
     
        if (hit.collider != null)
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

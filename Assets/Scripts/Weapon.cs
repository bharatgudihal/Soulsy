using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Weapon : ScriptableObject {
    public Sprite sprite;
    public float weaponAttackWindowStart;
    public float weaponAttackWindowEnd;
    public float damage;
    public Effect effect;
    public Vector2 colliderOffset;
    public Vector2 colliderSize;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gun name here", menuName = "Guns/Projectiles")]
public class Gun_Projectile : ScriptableObject
{
    public GameObject _Projectile;
    public float _ProjectileSpeed;
    public float _gravityPull;
    public float _damage;
    public bool _explodes;
    public GameObject _explosion;
    public float _explosionRadius;
    public LayerMask _explosionLayer;
}

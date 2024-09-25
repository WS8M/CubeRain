using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Explosion
{
    private const float UpwardsModifer = 0;
    
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private Transform _transform;
    
    public void Activate()
    {
        List<Collider> hits = Physics.OverlapSphere(_transform.position, _explosionRadius).ToList();

        foreach (var hit in hits)
            if(hit.attachedRigidbody != null)
                hit.attachedRigidbody.AddExplosionForce(_explosionForce, _transform.position, _explosionRadius,UpwardsModifer, ForceMode.Impulse);
    }
}
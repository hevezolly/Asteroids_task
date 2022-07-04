using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolledObject))]
public class ExplosionObject : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    private PoolledObject pooledObj;

    private void Awake()
    {
        pooledObj = GetComponent<PoolledObject>();
        pooledObj.SpawnEvent.AddListener(OnSpawn);
    }

    public void OnSpawn()
    {
        particles.Play();
    }

    private void OnParticleSystemStopped()
    {
        pooledObj.Despawn();
    }
}

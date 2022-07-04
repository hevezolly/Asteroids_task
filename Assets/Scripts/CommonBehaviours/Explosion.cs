using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private SpawnObjectRequest explosionSpawner;

    public void Explode()
    {
        explosionSpawner.Spawn(transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StraighMovement))]
[RequireComponent(typeof(PoolledObject))]
public class AsteroidSplit : MonoBehaviour
{
    [SerializeField]
    private SpawnObjectRequest smallerPartSpawner;
    [SerializeField]
    private float splitAngle = 45;
    [SerializeField]
    private Vector2 partVelocityRange;

    private StraighMovement movement;
    private PoolledObject pooledObj;

    private Quaternion leftRotation;
    private Quaternion rightRotation;

    private void Awake()
    {
        movement = GetComponent<StraighMovement>();
        pooledObj = GetComponent<PoolledObject>();
        leftRotation = Quaternion.AngleAxis(splitAngle, Vector3.forward);
        rightRotation = Quaternion.AngleAxis(-splitAngle, Vector3.forward);
    }

    public void Split()
    {
        var direction = movement.Velocity.normalized;
        var speed = Random.Range(partVelocityRange.x, partVelocityRange.y);

        SpawnPart(leftRotation * direction * speed);
        SpawnPart(rightRotation * direction * speed);

        pooledObj.Despawn();
    }

    private void SpawnPart(Vector2 velocity)
    {
        smallerPartSpawner
            .Spawn(transform.position, Quaternion.identity)?
            .GetComponent<StraighMovement>().SetMovement(velocity);
    }


}

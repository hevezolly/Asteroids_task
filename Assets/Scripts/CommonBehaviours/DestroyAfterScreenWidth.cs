using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolledObject), typeof(StraighMovement))]
public class DestroyAfterScreenWidth : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<Rect> fieldSize;
    private PoolledObject pooledObj;
    private StraighMovement movement;

    void Start()
    {
        movement = GetComponent<StraighMovement>();
        pooledObj = GetComponent<PoolledObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.DistanceTraveled > fieldSize.Value.width)
        {
            pooledObj.Despawn();
        }
    }
}

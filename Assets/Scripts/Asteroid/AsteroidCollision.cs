using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidCollision : MonoBehaviour
{
    [SerializeField]
    private AsteroidInterractionType defaultInterraction;

    public UnityEvent<bool> destroyEvent;
    public UnityEvent<bool> splitEvent;

    private bool wasCollision = false;
    private bool playerInitiated;
    private AsteroidInterractionType currentCollisionType; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        wasCollision = true;
        playerInitiated = collision.collider.GetComponent<PlayerInitiatedObject>() != null;
        currentCollisionType = defaultInterraction;
        if (collision.collider.TryGetComponent<AsteroidInterraction>(out var interraction))
        {
            currentCollisionType = interraction.Type;
        }
    }

    private void Update()
    {
        if (wasCollision)
        {
            switch (currentCollisionType)
            {
                case AsteroidInterractionType.Destroy:
                    destroyEvent?.Invoke(playerInitiated);
                    break;
                case AsteroidInterractionType.Split:
                    splitEvent?.Invoke(playerInitiated);
                    break;
            }
            wasCollision = false;
        }
    }
}

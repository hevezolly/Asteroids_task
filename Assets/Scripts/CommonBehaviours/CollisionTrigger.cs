using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent<bool> CollisionEvent;

    private bool wasCollision = false;
    private bool playerInitiated = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        wasCollision = true;
        playerInitiated = collision.collider.GetComponent<PlayerInitiatedObject>() != null;
    }

    private void Update()
    {
        if (wasCollision)
        {
            CollisionEvent?.Invoke(playerInitiated);
            wasCollision = false;
        }
    }

}

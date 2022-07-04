using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraighMovement : MonoBehaviour
{
    public float DistanceTraveled { get; private set; } = 0;

    private Vector3 velocity;
    private float speed;

    public Vector2 Velocity => velocity;

    public void SetMovement(Vector2 velocity)
    {
        this.velocity = velocity;
        speed = velocity.magnitude;
    }

    public void Stop()
    {
        SetMovement(Vector2.zero);
    }

    public void ResetDistance()
    {
        DistanceTraveled = 0;
    }

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
        DistanceTraveled += speed * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerInputSchema inputSchema;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private ScriptableValueField<float> maxSpeed;

    [SerializeField]
    [Tooltip("rotation speed in degrees per second")]
    private float rotationSpeed;

    private Vector3 currentSpeed;

    public UnityEvent StartThrustEvent;
    public UnityEvent StopThrustEvent;

    private bool isAccelerating;


    public void Stop()
    {
        currentSpeed = Vector3.zero;
    }

    void Start()
    {
        inputSchema.Initiate(transform);
        isAccelerating = inputSchema.GetAcceleration() > Mathf.Epsilon;
    }

    private void Rotate()
    {
        var rotationDeltaRadians = rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
        var newUpDirection = Vector3.RotateTowards(transform.up, inputSchema.GetDisiredDirection(), rotationDeltaRadians, 0);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, newUpDirection);
    }

    private void InvokeAccelerationEvents()
    {
        var currentAcceleration = inputSchema.GetAcceleration() > Mathf.Epsilon;
        if (currentAcceleration != isAccelerating)
        {
            if (currentAcceleration)
                StartThrustEvent?.Invoke();
            else
                StopThrustEvent?.Invoke();
        }

        isAccelerating = currentAcceleration;
    }

    private void Accelerate()
    {
        var moveSpeedDelta = transform.up * inputSchema.GetAcceleration() * acceleration * Time.deltaTime;
        currentSpeed += moveSpeedDelta;
        var velocity = Mathf.Min(currentSpeed.magnitude, maxSpeed.Value);
        currentSpeed = velocity * currentSpeed.normalized;
    }

    void Update()
    {
        Rotate();
        Accelerate();
        InvokeAccelerationEvents();

        transform.position += currentSpeed * Time.deltaTime;
    }
}

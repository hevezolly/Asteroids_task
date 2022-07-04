using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField]
    private Vector2 minMaxRotationSpeed;

    private float rotationSpeed;
    
    private void OnEnable()
    {
        rotationSpeed = Random.Range(minMaxRotationSpeed.x, minMaxRotationSpeed.y);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

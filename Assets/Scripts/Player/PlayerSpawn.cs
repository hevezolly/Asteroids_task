using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private Vector2 position;
    private Quaternion rotation;

    private void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    public void Respawn()
    {
        transform.SetPositionAndRotation(position, rotation);
    }
}

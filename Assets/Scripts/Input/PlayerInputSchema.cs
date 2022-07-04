using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Defines input, used for player controll
/// </summary>
public abstract class PlayerInputSchema : ScriptableObject
{
    protected Transform playerTransfrom { get; private set; }
    public void Initiate(Transform player)
    {
        playerTransfrom = player;
        Init();
    }

    protected virtual void Init() { }

    public abstract Vector2 GetDisiredDirection();

    public abstract float GetAcceleration();

    public abstract bool GetShot();
}

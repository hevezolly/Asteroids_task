using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidInterraction : MonoBehaviour
{
    [SerializeField]
    private AsteroidInterractionType type;

    public AsteroidInterractionType Type => type;
}

public enum AsteroidInterractionType
{
    Split,
    Destroy
}

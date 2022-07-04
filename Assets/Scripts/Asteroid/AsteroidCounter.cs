using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCounter : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<int> asteroidCount;

    public void OnSpawn()
    {
        asteroidCount.Value += 1;
    }

    public void OnDespawn()
    {
        asteroidCount.Value -= 1;
    }
}

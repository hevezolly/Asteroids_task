using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int initialNumberOfHealth;
    [SerializeField]
    private ScriptableValue<int> playerHealth;

    private void Awake()
    {
        playerHealth.Value = initialNumberOfHealth;
    }

    public void DecreaseHealth()
    {
        if (playerHealth.Value > 0)
            playerHealth.Value--;
    }

}

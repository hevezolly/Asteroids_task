using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{

    [SerializeField]
    private ScriptableValue<int> playerHealth;

    [SerializeField]
    private GameObject HealthPointObject;

    private int previusPlayerHelth;

    private Stack<GameObject> activeHealth;

    private void Start()
    {
        activeHealth = new Stack<GameObject>();
        previusPlayerHelth = playerHealth.Value;
        for (var i = 0; i < previusPlayerHelth; i++)
        {
            activeHealth.Push(Instantiate(HealthPointObject, transform));
        }
    }


    private void OnHealthChanged(int newHealth)
    {
        var diff = previusPlayerHelth - newHealth;
        // player health won't be restored;
        if (diff < 0)
            return;

        for (var i = 0; i < Mathf.Min(diff, activeHealth.Count); i++)
        {
            var obj = activeHealth.Pop();
            obj.SetActive(false);
        }

        previusPlayerHelth = newHealth;
    } 

    private void OnEnable()
    {
        playerHealth.ValueChangeEvent.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        playerHealth.ValueChangeEvent.RemoveListener(OnHealthChanged);
    }
}

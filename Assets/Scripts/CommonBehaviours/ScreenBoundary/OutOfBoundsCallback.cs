using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OutOfBoundsCallback : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<Rect> fieldRect;

    [SerializeField]
    [Tooltip("how far should objects center go out of bounds to trigger callback")]
    private float margin;

    public UnityEvent OutOfBoundsEvent;

    private Rect extendedRect;

    private bool isInBounds;

    private void Start()
    {
        extendedRect = GetExtendedRect();   
    }

    public Rect GetExtendedRect()
    {
        return new Rect(fieldRect.Value.x - margin, fieldRect.Value.y - margin,
            fieldRect.Value.width + margin * 2, fieldRect.Value.height + margin * 2);
    }

    void Update()
    {
        var currentOutOfBoundsState = extendedRect.Contains(transform.position);

        if (!currentOutOfBoundsState)
        {
            OutOfBoundsEvent?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, margin);
        if (fieldRect == null)
            return;
        Gizmos.color = Color.red;
        var updatedRect = GetExtendedRect();
        Gizmos.DrawWireCube(updatedRect.center, updatedRect.size);
    }
}

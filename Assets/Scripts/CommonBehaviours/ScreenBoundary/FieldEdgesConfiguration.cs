using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEdgesConfiguration : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private ScriptableValueField<Rect> fieldEdges;

    void Awake()
    {
        var bottomLeft = camera.ViewportToWorldPoint(Vector3.zero);
        var tobRight = camera.ViewportToWorldPoint(Vector3.one);

        fieldEdges.Value = new Rect(bottomLeft, tobRight - bottomLeft);
    }

    private void OnDrawGizmos()
    {
        if (!fieldEdges.HasValue)
            return;
        Gizmos.DrawWireCube(fieldEdges.Value.center, fieldEdges.Value.size);
    }
}

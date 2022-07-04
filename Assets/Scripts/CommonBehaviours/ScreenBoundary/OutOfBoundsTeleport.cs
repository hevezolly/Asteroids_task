using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutOfBoundsCallback))]
public class OutOfBoundsTeleport : MonoBehaviour
{
    private OutOfBoundsCallback outOfBoundsChecker;

    private Rect extendedRect;

    private void Awake()
    {
        outOfBoundsChecker = GetComponent<OutOfBoundsCallback>();
    }

    private void Start()
    {
        extendedRect = outOfBoundsChecker.GetExtendedRect();
    }

    private void OnEnable()
    {
        outOfBoundsChecker.OutOfBoundsEvent.AddListener(Teleport);
    }

    private void OnDisable()
    {
        outOfBoundsChecker.OutOfBoundsEvent.RemoveListener(Teleport);
    }

    private float GetTeleportCoordinateSingleAxis(float position, float min, float max)
    {
        if (position < min)
        {
            var offset = position - min;
            return max + offset;
        }
        if (position > max)
        {
            var offset = position - max;
            return min + offset;
        }
        return position;
    }

    private void Teleport()
    {
        var x = GetTeleportCoordinateSingleAxis(transform.position.x, extendedRect.xMin, extendedRect.xMax);
        var y = GetTeleportCoordinateSingleAxis(transform.position.y, extendedRect.yMin, extendedRect.yMax);
        transform.position = new Vector2(x, y);
    }
}

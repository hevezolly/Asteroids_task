using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityFrames : MonoBehaviour
{
    [SerializeField]
    private float invincibilityTime;

    [SerializeField]
    [Tooltip("number of blinks per second")]
    private float blinkingRate;

    [SerializeField]
    private List<Collider2D> colliders;

    [SerializeField]
    private List<Renderer> renderers;

    private Coroutine blinkingCoroutine;
    private Coroutine invincibilityCoroutine;

    private WaitForSeconds invincibilityWati;
    private WaitForSeconds blinkWait;

    private void Awake()
    {
        invincibilityWati = new WaitForSeconds(invincibilityTime);
        blinkWait = new WaitForSeconds(1 / blinkingRate);
    }

    private void Start()
    {
        StartInvincibility();
    }

    public void StartInvincibility()
    {
        invincibilityCoroutine = StartCoroutine(Invincibility());
    }

    private void OnDisable()
    {
        if (invincibilityCoroutine != null)
            StopCoroutine(invincibilityCoroutine);
        if (blinkingCoroutine != null)
            StopCoroutine(blinkingCoroutine);
    }

    private void SetCollidersState(bool state)
    {
        foreach (var c in colliders)
        {
            c.enabled = state;
        }
    }

    private void SetRenderersState(bool state)
    {
        foreach (var r in renderers)
        {
            r.enabled = state;
        }
    }

    private IEnumerator Invincibility()
    {
        blinkingCoroutine = StartCoroutine(Blinking());
        SetCollidersState(false);
        yield return invincibilityWati;
        StopCoroutine(blinkingCoroutine);
        SetCollidersState(true);
        SetRenderersState(true);
    }

    private IEnumerator Blinking()
    {
        var state = false;
        while (true)
        {
            SetRenderersState(state);
            state = !state;
            yield return blinkWait;
        }
    }

}

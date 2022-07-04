using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private SpawnObjectRequest spawnBuletRequest;

    [SerializeField]
    private PlayerInputSchema inputSchema;

    [SerializeField]
    [Tooltip("number of shots per second")]
    private float shootRate;

    [SerializeField]
    private ScriptableValueField<float> buletSpeed;

    [SerializeField]
    private Transform shootPoint;

    private Coroutine shotLoop;
    private WaitForSeconds betweenShotsTime;

    public UnityEvent<Vector2> ShotEvent;

    private void Awake()
    {
        betweenShotsTime = new WaitForSeconds(1 / shootRate);
        
    }

    private void OnEnable()
    {
        shotLoop = StartCoroutine(ShotLoop());
    }

    private IEnumerator ShotLoop()
    {
        while (true)
        {
            if (inputSchema.GetShot())
            {
                Shot();
                yield return betweenShotsTime;
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(shotLoop);
    }

    private void Shot()
    {
        var buletObj = spawnBuletRequest.Spawn(shootPoint.position, shootPoint.rotation);
        if (buletObj == null)
            return;
        buletObj.GetComponent<StraighMovement>()
            .SetMovement(shootPoint.up * buletSpeed.Value);
        ShotEvent?.Invoke(shootPoint.position);
    }
}

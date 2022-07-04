using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField]
    private StraighMovement ufo;

    [SerializeField]
    private ScriptableValue<Rect> fieldRect;

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("how tall the ufo spawn area. 0 - spawns only at horizontal center line, 1 - spawns at any height")]
    private float spawnHightPercent;

    [SerializeField]
    [Tooltip("how far off screen ufo spawns")]
    private float offset;

    [SerializeField]
    private Vector2 minMaxUFOSpawnDelay;

    [SerializeField]
    [Tooltip("range of time in which ufo travels through entire screen")]
    private Vector2 minMaxScreenTravelTime;

    private Coroutine ufoSpawnCountdownCoroutine;

    private void Awake()
    {
        ufo.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartUFOSpawnCountdown();
    }

    private void SpawnUFO()
    {
        var side = Random.Range(0, 2);
        if (side == 0)
        {
            SpawnAtOneSide(fieldRect.Value.xMin - offset, Vector2.right);
        }
        else
        {
            SpawnAtOneSide(fieldRect.Value.xMax + offset, Vector2.left);
        }
    }

    private void SpawnAtOneSide(float sideX, Vector2 travelDirection)
    {
        var (bottom, top) = GetVerticalSpawnRange();
        var ySpawnPosition = Random.Range(bottom, top);
        var spawnPosition = new Vector2(sideX, ySpawnPosition);


        ufo.transform.position = spawnPosition;
        var velocity = travelDirection * fieldRect.Value.size.x / Random.Range(minMaxScreenTravelTime.x, minMaxScreenTravelTime.y);

        ufo.SetMovement(velocity);
        ufo.gameObject.SetActive(true);
    }

    public void StartUFOSpawnCountdown()
    {
        ufoSpawnCountdownCoroutine = StartCoroutine(UfoCountdown());
    }

    private IEnumerator UfoCountdown()
    {
        yield return new WaitForSeconds(Random.Range(minMaxUFOSpawnDelay.x, minMaxUFOSpawnDelay.y));
        SpawnUFO();
    }

    private void OnDisable()
    {
        if (ufoSpawnCountdownCoroutine != null)
        {
            StopCoroutine(ufoSpawnCountdownCoroutine);
        }
    }


    private (float, float) GetVerticalSpawnRange()
    {
        var top = fieldRect.Value.center.y + (fieldRect.Value.size.y * spawnHightPercent) / 2;
        var bottom = fieldRect.Value.center.y - (fieldRect.Value.size.y * spawnHightPercent) / 2;
        return (bottom, top);
    }


    private void OnDrawGizmosSelected()
    {
        if (fieldRect == null)
            return;
        Gizmos.color = Color.blue;
        var (bottom, top) = GetVerticalSpawnRange();

        var lineStart = new Vector2(fieldRect.Value.xMin - offset, bottom);
        var lineEnd = new Vector2(fieldRect.Value.xMin - offset, top);
        Gizmos.DrawLine(lineStart, lineEnd);

        lineStart = new Vector2(fieldRect.Value.xMax + offset, bottom);
        lineEnd = new Vector2(fieldRect.Value.xMax + offset, top);
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<Rect> fieldBoundary;
    [SerializeField]
    [Tooltip("how far out of screen asteroids should be spawned")]
    [Min(0.001f)]
    private float offset;

    [SerializeField]
    private SpawnObjectRequest asteroidSpawner;
    [SerializeField]
    private int nextNumberOfSpawnAsteroids;
    [SerializeField]
    private Vector2 minMaxAsteroidInitialSpeed;
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("how much new asteroids tend to fly to center. 0 - direction random, 1 - direction towards center")]
    private float tendTowardsCenter;
    [SerializeField]
    private ScriptableValue<int> currentNumberOfAsteroids;
    [SerializeField]
    private float delayBetweenWaves;

    private WaitForSeconds newAsteroidsSpawnDelay;

    private void Awake()
    {
        newAsteroidsSpawnDelay = new WaitForSeconds(delayBetweenWaves);
        currentNumberOfAsteroids.Value = 0;
    }

    private void Start()
    {
        SpawnAsteroids();
    }

    private void OnEnable()
    {
        currentNumberOfAsteroids.ValueChangeEvent.AddListener(OnNumberOfAsteroidsChanged);
    }

    private void OnDisable()
    {
        currentNumberOfAsteroids.ValueChangeEvent.RemoveListener(OnNumberOfAsteroidsChanged);
    }

    private IEnumerator WaitAndSpawnNewWave()
    {
        yield return newAsteroidsSpawnDelay;
        SpawnAsteroids();
    }

    private void OnNumberOfAsteroidsChanged(int number)
    {
        if (number != 0)
        {
            return;
        }
        StartCoroutine(WaitAndSpawnNewWave());
    }

    public void SpawnAsteroids()
    {
        for (var i = 0; i < nextNumberOfSpawnAsteroids; i++)
        {
            var (start, end) = SelectLineSegment();
            SpawnAsteroidAtLineSegment(start, end);
        }
        nextNumberOfSpawnAsteroids++;
    }

    private (Vector2, Vector2) SelectLineSegment()
    {
        var bl = new Vector2(fieldBoundary.Value.xMin - offset, fieldBoundary.Value.yMin - offset);
        var br = new Vector2(fieldBoundary.Value.xMax + offset, fieldBoundary.Value.yMin - offset);
        var tl = new Vector2(fieldBoundary.Value.xMin - offset, fieldBoundary.Value.yMax + offset);
        var tr = new Vector2(fieldBoundary.Value.xMax + offset, fieldBoundary.Value.yMax + offset);
        var randomVariant = Random.Range(0, 4);
        switch (randomVariant)
        {
            case 0:
                return (bl, br);
            case 1:
                return (bl, tl);
            case 2:
                return (tl, tr);
            default:
                return (tr, br);
        }
    }

    private void SpawnAsteroidAtLineSegment(Vector2 start, Vector2 end)
    {
        var spawnPosition = Vector2.Lerp(start, end, Random.Range(0f, 1f));
        var spawnedObject = asteroidSpawner.Spawn(spawnPosition, Quaternion.identity);
        if (spawnPosition == null)
            return;
        var direction = SelectMoveDirection(spawnPosition);
        var speed = Random.Range(minMaxAsteroidInitialSpeed.x, minMaxAsteroidInitialSpeed.y);

        spawnedObject.GetComponent<StraighMovement>().SetMovement(direction * speed);
    }

    private Vector2 SelectMoveDirection(Vector2 spawnPosition)
    {
        //Iterate through all pairs of directions from spawn position to game field corners.
        //Select pair with smallest dot product. This is two extreme directions,
        //result direction should be between them.

        var minVal = float.MaxValue;
        var startDir = Vector2.zero;
        var endDir = Vector2.zero;

        foreach (var (start, end) in GetAllDirectionPairs(spawnPosition))
        {
            var dot = Vector2.Dot(start, end);
            if (dot < minVal)
            {
                minVal = dot;
                startDir = start;
                endDir = end;
            }
        }

        var t = Random.Range(0f + 0.499f * tendTowardsCenter, 1f - 0.499f * tendTowardsCenter);

        //calculate intermediate direction through rotations to meke destribution uniform; 
        var startRot = Quaternion.LookRotation(Vector3.forward, startDir);
        var endRot = Quaternion.LookRotation(Vector3.forward, endDir);

        var resultRotation = Quaternion.Lerp(startRot, endRot, t);

        var direction = resultRotation * Vector2.up;
        return direction;
    }

    private IEnumerable<(Vector2, Vector2)> GetAllDirectionPairs(Vector2 from)
    {
        // directions to corners of game field
        var bl = (new Vector2(fieldBoundary.Value.xMin, fieldBoundary.Value.yMin) - from).normalized;
        var br = (new Vector2(fieldBoundary.Value.xMax, fieldBoundary.Value.yMin) - from).normalized;
        var tl = (new Vector2(fieldBoundary.Value.xMin, fieldBoundary.Value.yMax) - from).normalized;
        var tr = (new Vector2(fieldBoundary.Value.xMax, fieldBoundary.Value.yMax) - from).normalized;

        // all combinations
        yield return (bl, br);
        yield return (bl, tl);
        yield return (bl, tr);
        yield return (br, tl);
        yield return (br, tr);
        yield return (tl, tr);
    }


    private void OnDrawGizmosSelected()
    {
        if (fieldBoundary == null)
            return;
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(fieldBoundary.Value.center, fieldBoundary.Value.size + Vector2.one * 2 * offset);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UFOShooting : MonoBehaviour
{
    [SerializeField]
    private SpawnObjectRequest spawnUFOBullet;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private ScriptableValueField<float> bulletSpeed;
    [SerializeField]
    private Vector2 minMaxShotDelay;
    [SerializeField]
    [Tooltip("how far from ufo should bullet be spawned")]
    [Min(0)]
    private float bulletSpawnOffset;
    [SerializeField]
    private Transform bulletSpawnCenter;

    public UnityEvent<Vector2> ShotEvent;


    private Coroutine shootingProcess;

    private void OnEnable()
    {
        shootingProcess = StartCoroutine(ShootingProcess());
    }

    private void OnDisable()
    {
        if (shootingProcess != null)
            StopCoroutine(shootingProcess);
    }

    private IEnumerator ShootingProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minMaxShotDelay.x, minMaxShotDelay.y));
            Shoot();
        }
    }

    private void Shoot()
    {
        var direction = (player.position - bulletSpawnCenter.position).normalized;
        var spawnPosition = bulletSpawnCenter.position + direction * bulletSpawnOffset;

        var bulletRotation = Quaternion.LookRotation(Vector3.forward, direction);

        var bullet = spawnUFOBullet.Spawn(spawnPosition, bulletRotation).GetComponent<StraighMovement>();

        bullet.SetMovement(direction * bulletSpeed.Value);
        ShotEvent?.Invoke(spawnPosition);
    }

    private void OnDrawGizmosSelected()
    {
        if (bulletSpawnCenter == null)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(bulletSpawnCenter.position, bulletSpawnOffset);
    }

}

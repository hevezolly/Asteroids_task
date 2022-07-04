using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolledObject))]
public class AudioEffect : MonoBehaviour
{
    private PoolledObject pooledObj;
    [SerializeField]
    private AudioSource source;

    private bool isPlaying;
    private void Awake()
    {
        pooledObj = GetComponent<PoolledObject>();
        isPlaying = false;
    }

    public void OnDespawn()
    {
        isPlaying = false;
        source.Stop();
    }

    public void Play(AudioClip clip)
    {
        source.clip = clip;
        isPlaying = true;
        source.Play();
    }

    private void Update()
    {
        if (isPlaying && !source.isPlaying)
        {
            pooledObj.Despawn();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectPlayer : MonoBehaviour
{
    [SerializeField]
    private SpawnObjectRequest audioEffectSpawner;
    [SerializeField]
    private AudioClip clip;
    public void Play(Vector2 at)
    {
        audioEffectSpawner.Spawn(at, Quaternion.identity).GetComponent<AudioEffect>().Play(clip);
    }

    public void Play()
    {
        Play(Vector2.zero);
    }
}

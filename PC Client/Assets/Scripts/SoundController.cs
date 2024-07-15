using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    public AudioSource fxSource;
    public AudioSource BGMSource;

    public AudioClip collisionClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayCollisionSound()
    {
        fxSource.PlayOneShot(collisionClip);
    }
}

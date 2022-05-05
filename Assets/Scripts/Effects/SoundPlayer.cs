using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : Singleton<SoundPlayer>
{
    
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip craftClip;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayHitClip()
    {
        audioSource.PlayOneShot(hitClip);
    }
    public void PlayScraftClip()
    {
        audioSource.PlayOneShot(craftClip);
    }
}

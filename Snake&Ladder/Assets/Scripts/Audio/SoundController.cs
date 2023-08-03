using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!instance)
            instance = this;
        else Destroy(this);
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    internal void PlaySoundOnce(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    //internal void PlayorPauseSound()

}

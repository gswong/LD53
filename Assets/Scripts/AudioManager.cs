using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip accomplished;

    public AudioSource MyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        SetDontDestroy();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccomplishedSound()
    {
        Debug.Log($"{nameof(AudioManager)}.{nameof(AccomplishedSound)} is playing the sound effect.s");
        MyAudioSource.PlayOneShot(accomplished);
    }
}

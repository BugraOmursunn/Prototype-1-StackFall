using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource _audioSource;
    private void Awake()
    {
        makeSingleton();
        _audioSource = GetComponent<AudioSource>();
    }
    private void makeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public void playSoundFx(AudioClip _audioClip, float volume)
    {
        _audioSource.PlayOneShot(_audioClip, volume);
    }
}

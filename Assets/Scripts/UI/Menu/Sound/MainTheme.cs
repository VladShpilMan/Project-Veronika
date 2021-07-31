using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTheme : MonoBehaviour
{
    [SerializeField] private AudioClip _music;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private SoundVolume _sound;

    private void Start()
    {
        _audioSource.PlayOneShot(_music, 0.25F);

        _sound.sound += SoundSwitch;
    }

    private void SoundSwitch(float volume)
    {
        _audioSource.volume = volume;
    }

}

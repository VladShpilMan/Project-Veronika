using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTheme : MonoBehaviour
{
    [SerializeField] private AudioClip _music;

    [SerializeField] private AudioSource _audioSource; 
    
    private void Start()
    {
        _audioSource.PlayOneShot(_music, 1);
    }

    private void Update()
    {
        _audioSource.volume = SoundVolume.Volume;
    }

}

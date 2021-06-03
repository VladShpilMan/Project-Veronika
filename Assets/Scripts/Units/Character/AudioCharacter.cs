using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacter : MonoBehaviour
{
    private AudioSource clip;

    private void Start()
    {
        clip = GetComponentInChildren<AudioSource>();
    }

    private void FixedUpdate()
    {
        AudioManager();
    }

    private void AudioManager() {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    private AudioSource clip;

    private Character getIsMove;
    private bool isMove;

    private void Start()
    {
        //clip = GetComponentInChildren<AudioSource>();
        //Debug.Log(getIsMove.IsMove);
    }

    private void FixedUpdate()
    {
        AudioManager();
        //isMove = getIsMove.IsMove;
        //Debug.Log(getIsMove.IsMove);
    }

    private void AudioManager()
    {

    }
}
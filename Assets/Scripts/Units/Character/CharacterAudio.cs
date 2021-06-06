using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    private AudioSource clip;

    private bool setIsMove, setIsGround;

    private void Awake()
    {
        clip = GetComponentInChildren<AudioSource>();       
    }

    private void Update() {

        StepManager();
    }

    private void StepManager() {
        setIsMove = Character.IsMove;
        setIsGround = Character.IsGround;
 
        if (setIsMove && setIsGround) clip.mute = false;
            else clip.mute = true;

    }
}
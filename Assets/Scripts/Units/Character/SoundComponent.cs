using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour {

    private AudioClip _attack; 

    private AudioSource _source;

    #region MONO
    public void SoundStart(InputComponent input)
    {
        input.someSound += SwitchSound;

        SettingSounds();
        GetReferences();
    }

    private void GetReferences()
    {
        _attack = Resources.Load<AudioClip>("Cut/PlayerSword");
    }

    private void SettingSounds()
    {
        _source = GetComponent<AudioSource>();

        _source.playOnAwake = false;
        _source.mute = false;
        _source.loop = false;
    }
    #endregion


    private void SwitchSound(string argument)
    {
        switch (argument)
        {
            case "Attack":
                Debug.Log(_attack);
                AttakSound();
                break;
            case "Move":
                
                break;
        }
    }

    private void AttakSound()
    {
        _source.PlayOneShot(_attack, 0.1f);
    }
}

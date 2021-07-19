using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour {

    private AudioClip _attack, _jump, _getSword, _hit; 

    private AudioSource _source;

    #region MONO
    public void SoundStart(Player player, InputComponent input)
    {
        input.someSound += SwitchSound;
        player.hitSound += SwitchSound;

        SettingSounds();
        GetReferences();
    }

    private void GetReferences()
    {
        _attack = Resources.Load<AudioClip>("Cut/PlayerSword");
        _jump = Resources.Load<AudioClip>("Jump");
        _getSword = Resources.Load<AudioClip>("Cut/GetSword"); 
        _hit = Resources.Load<AudioClip>("Hit");
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
            case "Jump":
                JumpSound();
                break;
            case "GetSword":
                GetSword();
                break;
            case "Hit":
                HitSound();
                break;
        }
    }

    #region PRIVATE
    private void AttakSound()
    {
        _source.PlayOneShot(_attack, 0.1f);
    }

    private void JumpSound()
    {
        _source.PlayOneShot(_jump, 1f);
    }

    private void GetSword()
    {
        _source.PlayOneShot(_getSword, 0.3f);
    }

    private void HitSound()
    {
        _source.PlayOneShot(_hit, 0.3f);
    }
    #endregion
}

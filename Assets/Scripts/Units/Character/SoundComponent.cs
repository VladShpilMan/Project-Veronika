using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    [SerializeField]private InputComponent _input;

    private AudioClip _attack;

    private string mainFolder = "Footsteps", cutFoldet = "Weapons and hand tools";

    public void SoundStart(InputComponent input)
    {
        input.someSound += SwitchSound;

        _attack = Resources.Load<AudioClip>(mainFolder + "/" + cutFoldet);
    }

    private void SwitchSound(string argument)
    {
        switch (argument)
        {
            case "Attack":
                Debug.Log("Cut");
                AttakSound();
                break;
        }
    }

    private void AttakSound()
    {

    }
}

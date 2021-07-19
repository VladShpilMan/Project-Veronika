using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSound : MonoBehaviour
{
    [SerializeField] private AudioSource _myFx;
    [SerializeField] private AudioClip _hoverFx, _clickFx;

    public void HoverSound()
    {
        _myFx.PlayOneShot(_hoverFx, 0.3F);
    }

    public void ClickSound()
    {
        _myFx.PlayOneShot(_clickFx, 0.3F);
    }
}

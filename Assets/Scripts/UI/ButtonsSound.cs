using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSound : MonoBehaviour
{
    [SerializeField] private AudioSource _myFx;
    [SerializeField] private AudioClip _hoverFx, _clickFx;

    [SerializeField]private GameObject line;

    public void HoverSound()
    {
        _myFx.PlayOneShot(_hoverFx, 0.3F);       
    }

    public void ClickSound()
    {
        _myFx.PlayOneShot(_clickFx, 0.3F);
    }

    public void MouseEnter()
    {
        line.SetActive(true);
    }

    public void MouseExit()
    {
        line.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    public delegate void Sound(float volume);
    public event Sound sound;

    [SerializeField] private static float _volume;
    [SerializeField] private Slider _slider;
    private float _value;

    public static float Volume => _volume;

    private void Start()
    {
        _value = _volume = _slider.value;
    }

    private void Update()
    {
        _volume = _slider.value;

        if (_volume != _value)
        {
            sound(_volume);
            _value = _volume;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [SerializeField] private static float _volume;
    [SerializeField] private Slider _slider;

    public static float Volume => _volume;

    private void Start()
    {
        _volume = _slider.value;
    }

    private void Update()
    {
       _volume = _slider.value;
    }
}

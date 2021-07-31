using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    private SaveLoad _saveLoad;

    private void Awake()
    {
        _saveLoad = FindObjectOfType<SaveLoad>();
    }

    private void Start()
    {
        _saveLoad.objects.Add(this);
    }

    private void OnRemove()
    {
        _saveLoad.objects.Remove(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveInfo : MonoBehaviour
{
    private Saves _save;

    public Text uiText;

    private void Awake()
    {
        _save = FindObjectOfType<Saves>();
        _save.saveInfos.Add(this);
    }

    public void LoadSave()
    {

    }
}
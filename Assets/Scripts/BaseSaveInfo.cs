using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class BaseSaveInfo : MonoBehaviour
{

    [SerializeField] private SaveLoad saveLoad;
    private SaveInfo[] childScripts;

    private void Start()
    {
        childScripts = GetComponentsInChildren<SaveInfo>();
        foreach (SaveInfo child in childScripts)
        {
            saveLoad.saveButtons.Add(child);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]private GameObject _mainPanel;
    [SerializeField]private GameObject _optionsPanel;

    public void SwitchPanel()
    {
        if (_mainPanel.activeInHierarchy)
        {
            _mainPanel.SetActive(false);
            _optionsPanel.SetActive(true);
        }
        else
        {
            _mainPanel.SetActive(true);
            _optionsPanel.SetActive(false);
        }
    }
}

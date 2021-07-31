using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]private GameObject _mainPanel;
    [SerializeField]private GameObject _optionsPanel;
    [SerializeField] private GameObject _line;

    public void SwitchPanel()
    {
        if (_mainPanel.activeInHierarchy)
        {
            _mainPanel.SetActive(false);
            _optionsPanel.SetActive(true);
            _line.SetActive(false);
        }
        else
        {
            _mainPanel.SetActive(true);
            _optionsPanel.SetActive(false);
            _line.SetActive(false);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Saves : MonoBehaviour
{
    public List<SaveInfo> saveInfos = new List<SaveInfo>();

    private int _amount;
    private string[] _filesPath;
    private string[] _filesName;
    private Vector3 _position;
    private GameObject gameObject;
    private float y = 0;
    private RectTransform rectTransfromPanel;

    private void Start()
    {
        _filesPath = Directory.GetFiles(Application.persistentDataPath);
        rectTransfromPanel = this.GetComponent<RectTransform>();
        _filesName = new string[_filesPath.Length];



        rectTransfromPanel.sizeDelta = new Vector2(160f, 400f + (_filesPath.Length * 8f - 400f));

        Vector3 positionPlace = new Vector3(0, -7);
        Vector3 scaleText = new Vector3(0.5f, 0.25f, 1f);
        int counter = 0;
        foreach (string filename in _filesPath)
        {
            string temp = filename.Replace("/", @"\");
            _filesName[counter] = Path.GetFileName(temp);
            Instantiate(Resources.Load<GameObject>("SaveText"), positionPlace, Quaternion.identity, this.transform);

            counter++;
        }

        int count = 0;
        foreach (SaveInfo saveInfo in saveInfos)
        {

            RectTransform rectTransfromSave;
            rectTransfromPanel = saveInfo.GetComponent<RectTransform>();
            rectTransfromPanel.localPosition = positionPlace;
            Debug.Log(rectTransfromPanel.sizeDelta.y);
            saveInfo.uiText.text = _filesName[count];
            rectTransfromPanel.localScale = scaleText;
            positionPlace.y -= 8;
            count++;
        }
    }
}

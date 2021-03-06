using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Saves : MonoBehaviour
{
    #region Delegates
    public delegate void SubscribeDelegates();
    public event SubscribeDelegates subscribeDelegates;


    #endregion

    #region Collections
    public List<SaveInfo> saveInfos = new List<SaveInfo>();
    //public List<FileInfo> _filesPath = new List<FileInfo>();
    #endregion
    private int _amount;
    private string[] _filesPath;
    private string[] _filesName;
    private string[] _filesDate;
    private Vector3 _position;
    private GameObject gameObject;
    private float y = 0;
    private RectTransform rectTransfromPanel;

    [SerializeField]private SaveLoad _saveLoad;

    private void Start()
    {
        _saveLoad.generateSavePanel += GenerateSavesPanel;
        GenerateSavesPanel();
    }

    private void GenerateSavesPanel()
    {
        foreach (var item in saveInfos)
        {
            item.DestroySelf();
        }

        saveInfos.Clear();
        //saveGenerate();

        _filesPath = Directory.GetFiles(Application.persistentDataPath).OrderBy(d => new FileInfo(d).CreationTime).ToArray();

        rectTransfromPanel = this.GetComponent<RectTransform>();
        _filesName = new string[_filesPath.Length];
        _filesDate = new string[_filesPath.Length];


        rectTransfromPanel.sizeDelta = new Vector2(160f, 400f + (_filesPath.Length * 8f - 400f));

        Vector3 positionPlace = new Vector3(-82, -7);
        //Vector3 scaleText = new Vector3(0.5f, 0.25f, 1f);
        Vector3 scaleText = new Vector3(1f, 1f, 1f);
        int counter = 0;
        foreach (string filename in _filesPath.Reverse())
        {
            string temp = filename.Replace("/", @"\");
            _filesName[counter] = Path.GetFileName(temp);
            _filesDate[counter] = File.GetCreationTime(filename).ToString();
            //Instantiate(Resources.Load<GameObject>("SaveText"), positionPlace, Quaternion.identity, this.transform);
            Instantiate(Resources.Load<GameObject>("SaveBG"), positionPlace, Quaternion.identity, this.transform);
            counter++;
        }

        int count = 0;
        foreach (SaveInfo saveInfo in saveInfos)
        {

            RectTransform rectTransfromSave;
            rectTransfromPanel = saveInfo.GetComponent<RectTransform>();
            rectTransfromPanel.localPosition = positionPlace;
            //Debug.Log(rectTransfromPanel.sizeDelta.y);
            saveInfo.uiText.text = _filesName[count];
            saveInfo.uiDate.text = _filesDate[count];
            rectTransfromPanel.localScale = scaleText;
            positionPlace.y -= 7;
            count++;
        }
        subscribeDelegates();
    }
}


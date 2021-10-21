using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class SaveInfo : BaseSaveInfo
{
    public delegate void Save(XmlElement root);
    public event Save save;

    private Saves _save;

    public Text uiText;

    private void Awake()
    {
        _save = FindObjectOfType<Saves>();
        _save.saveInfos.Add(this);
    }

    public virtual void LoadSave()
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(Application.persistentDataPath + "/" + uiText.text);
        XmlElement root = xDoc.DocumentElement;
        Debug.Log("Load " + uiText.text);
        //save(root);
        MailingSave(root);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

//public class SaveInfo : BaseSaveInfo
//{
//    public delegate void Save(XmlElement root);
//    public event Save save;

//    private Saves _save;

//    public Text uiText;

//    private void Awake()
//    {
//        _save = FindObjectOfType<Saves>();
//        _save.saveInfos.Add(this);       
//    }

//    public void LoadSave()
//    {
//        XmlDocument xDoc = new XmlDocument();
//        xDoc.Load(Application.persistentDataPath + "/" + uiText.text);
//        XmlElement root = xDoc.DocumentElement;
//        Debug.Log("Load " + uiText.text);
//        save(root);
//    }

//    public void DestroySelf()
//    {
//        Destroy(gameObject);
//    }
//}
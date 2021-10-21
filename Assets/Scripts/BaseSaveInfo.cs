using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Globalization;

public class BaseSaveInfo : MonoBehaviour
{

    public delegate void SendSave(XmlElement root);
    public event SendSave sendSave;

    private SaveLoad _saveLoad;

    private void Start()
    {
        _saveLoad = FindObjectOfType<SaveLoad>();
    }

    private void Update()
    {
        // Debug.Log(sendSave);
        //Debug.Log(Application.persistentDataPath);
    }

    public virtual void LoadSave()
    {

    } 

    protected void MailingSave(XmlElement root)
    {
        Debug.Log(root.InnerXml);
        //sendSave(root);
        GenerateScene(root);
    }

    private void GenerateScene(XmlElement root)
    {
        //SubscribeDelegates();
        foreach (var item in _saveLoad.objects)
        {
            item.Value.DestroySelf();
        }
        _saveLoad.objects.Clear();
        Debug.Log("Root " + root.InnerXml);
        foreach (XmlElement node in root)
        {
            XmlNode attrr = node.Attributes.GetNamedItem("isAlive");
            if (attrr.Value == "true")
            {
                Vector3 position = Vector3.zero;
                float health = 0;
                float maxHealth = 0;
                foreach (XmlNode chnode in node.ChildNodes)
                {

                    if (chnode.Name == "position")
                    {
                        position.x = float.Parse(chnode.Attributes.GetNamedItem("x").Value, CultureInfo.InvariantCulture);
                        position.y = float.Parse(chnode.Attributes.GetNamedItem("y").Value, CultureInfo.InvariantCulture);
                        position.z = float.Parse(chnode.Attributes.GetNamedItem("z").Value, CultureInfo.InvariantCulture);
                    }

                    if (chnode.Name == "health")
                    {
                        health = float.Parse(chnode.InnerText);
                        maxHealth = float.Parse(chnode.Attributes.GetNamedItem("maxHealth").Value, CultureInfo.InvariantCulture);
                    }
                }
                XmlNode attr = node.Attributes.GetNamedItem("name");

                Instantiate(Resources.Load<GameObject>(SplitString(attr.Value)), position, Quaternion.identity);

                StartCoroutine(Fade(attr, health, maxHealth));
            }
        }
    }

    public IEnumerator Fade(XmlNode attr, float health, float maxHealth)
    {
        yield return new WaitForSeconds(0.2f);
        _saveLoad.objects[attr.Value].SetHealth(health);
        _saveLoad.objects[attr.Value].SetMaxHealth(maxHealth);
    }

    private string SplitString(string splittable)
    {
        string[] stringResult = splittable.Split(' ');
        Debug.Log(stringResult[0]);

        return stringResult[0];
    }

}
//public class BaseSaveInfo : MonoBehaviour
//{

//    [SerializeField] private SaveLoad _saveLoad;
//    [SerializeField] private Saves _saveGenerate;
//    private SaveInfo[] childScripts;

//    private void Start()
//    {
//        childScripts = GetComponentsInChildren<SaveInfo>();
//    }

//    public void GetChild()
//    {
//        foreach (SaveInfo child in childScripts)
//        {
//            Debug.Log(child);
//            _saveLoad.saveButtons.Add(child);
//            Debug.Log("Add");
//        }
//    }


//    public void DestroySelf()
//    {
//        foreach (SaveInfo child in childScripts)
//        {
//            Destroy(child.gameObject);
//        }
//    }

//}

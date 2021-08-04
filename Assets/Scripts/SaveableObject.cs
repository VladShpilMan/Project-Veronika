using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class SaveableObject : MonoBehaviour
{
    private SaveLoad _saveLoad;

    public string objectName;

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

    public XElement GetElement()
    {
        XAttribute x = new XAttribute("x", transform.position.x);
        XAttribute y = new XAttribute("y", transform.position.y);
        XAttribute z = new XAttribute("z", transform.position.z);

        XElement element = new XElement("instance", objectName, x, y, z);

        return element;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}


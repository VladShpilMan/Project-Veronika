using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class SaveableObject : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private SaveLoad _saveLoad;

    [SerializeField] private string objectName;
    private string unitName;

    public string ObjectName => objectName;

    static int counter = 0;

    public XElement UnitHealth
    { 
        get 
        { 
            XElement healh = new XElement("health", unit.Health);
            return healh;
        }
        set
        {
            XElement hel = null;
            hel = value;
            unit.Health = (float)hel;
        }
    }

    private void Awake()
    {
        _saveLoad = FindObjectOfType<SaveLoad>();
    }

    private void Start()
    {

        unitName = objectName + " " + _saveLoad.objects.Count.ToString();
        string test = this.ToString();
        SplitString(unitName);
        _saveLoad.objects.Add(unitName, this);
    }

    private void OnRemove()
    {
        _saveLoad.objects.Remove(objectName);
    }

    public string GetUnit()
    {
        return unitName;
    }

    public bool GetAlive()
    {
        return unit.IsAlive; 
    }

    public XElement GetPosition()
    {
        XAttribute x = new XAttribute("x", transform.position.x);
        XAttribute y = new XAttribute("y", transform.position.y);
        XAttribute z = new XAttribute("z", transform.position.z);

        XElement position = new XElement("position", x, y, z);

        return position;
    }

    public XElement GetHealth()
    {
        //XAttribute isAlive = new XAttribute("isAlive", unit.IsAlive) ;
        XAttribute attr = new XAttribute("maxHealth", unit.MaxHealth);
        XElement healh = new XElement("health", unit.Health);
        //healh.Add(isAlive);
        healh.Add(attr);

        return healh;
    }

    public void SetHealth(float health)
    {
        unit.Health = health;
    }

    public void SetMaxHealth(float health)
    {
        unit.MaxHealth = health;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void SplitString(string splittable)
    {
        string[] stringResult = splittable.Split(' ');
        Debug.Log(stringResult[0]);
    }
}


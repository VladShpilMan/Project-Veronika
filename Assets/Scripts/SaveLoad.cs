using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Globalization;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{

    private string path;

    public Dictionary<string, SaveableObject> objects = new Dictionary<string, SaveableObject>();
    public List<SaveInfo> saveButtons = new List<SaveInfo>();

    [SerializeField] private InputField _inputField;

    private static int _saveCounter = 0;

    [SerializeField] private BaseSaveInfo saveInfo;

    private void Start()
    {
        path = Application.persistentDataPath + "/savedGames.xml";

        SubscribeDelegates();
    }

    private void SubscribeDelegates()
    {
        foreach (SaveInfo button in saveButtons)
        {
            button.save += GenerateScene;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) _Save();
        if (Input.GetKeyDown(KeyCode.Backspace)) Load();
    }

    public void _Save()
    {
        XElement units = new XElement("units");

        foreach (var obj in objects)
        {
            XElement unit = new XElement("unit");

            XAttribute name = new XAttribute("name", obj.Value.GetUnit());

            unit.Add(name);
            unit.Add(obj.Value.GetPosition());
            unit.Add(obj.Value.GetHealth());

            units.Add(unit);
        }

        XDocument xdoc = new XDocument(units);
        File.WriteAllText(path, xdoc.ToString());
    }

    public void NewSave()
    {
        if (_inputField.text != string.Empty)
            path = Application.persistentDataPath + "/" + _inputField.text;
        else
        {
            _saveCounter++;
            path = Application.persistentDataPath + $"/savedGames({_saveCounter}).xml";
        }

        XElement units = new XElement("units");
        XAttribute saveCounter = new XAttribute("saveCounter", _saveCounter);
        units.Add(saveCounter);

        foreach (var obj in objects)
        {
            XElement unit = new XElement("unit");

            XAttribute name = new XAttribute("name", obj.Value.GetUnit());

            unit.Add(name);
            unit.Add(obj.Value.GetPosition());
            unit.Add(obj.Value.GetHealth());

            units.Add(unit);
        }
        Debug.Log("Save");
        XDocument xdoc = new XDocument(units);
        File.WriteAllText(path, xdoc.ToString());
    }

    private void Load()
    {
        XmlDocument xDoc = new XmlDocument();

        xDoc.Load(path);
        XmlElement root = xDoc.DocumentElement;

        GenerateScene(root);
    }

    private void GenerateScene(XmlElement root)
    {
        Debug.Log("GenerateScene");
        foreach (var item in objects)
        {
            item.Value.DestroySelf();
        }
        objects.Clear();
        Debug.Log(root.Attributes.GetNamedItem("saveCounter"));
        foreach (XmlElement node in root)
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

    public IEnumerator Fade(XmlNode attr, float health, float maxHealth)
    {
        yield return new WaitForSeconds(0.2f);
        objects[attr.Value].SetHealth(health);
        objects[attr.Value].SetMaxHealth(maxHealth);
    }

    private string SplitString(string splittable)
    {
        string[] stringResult = splittable.Split(' ');
        Debug.Log(stringResult[0]);

        return stringResult[0];
    }
}
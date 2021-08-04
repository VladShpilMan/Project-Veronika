using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using System.Globalization;

public class SaveLoad : MonoBehaviour
{

    private string path;

    public List<SaveableObject> objects = new List<SaveableObject>();

    private void Start()
    {
        path = Application.persistentDataPath + "/savedGames.xml";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) Save();
        if (Input.GetKeyDown(KeyCode.Backspace)) Load();
    }

    private void Save()
    {
        XElement root = new XElement("root");

        foreach (SaveableObject obj in objects)
        {
            root.Add(obj.GetElement());
        }

        XDocument saveDoc = new XDocument(root);
        File.WriteAllText(path, saveDoc.ToString());
        Debug.Log(path);
    }

    private void Load()
    {
        XElement root = null;

        if (!File.Exists(path))
        {
            Debug.Log("Save data not found...");

            if (File.Exists(Application.persistentDataPath + "/savedGames.xml"))
                root = root = XDocument.Parse(File.ReadAllText(Application.persistentDataPath + "/savedGames.xml")).Element("root");
        }
        else
        {
            Debug.Log("Save data found...");
            root = XDocument.Parse(File.ReadAllText(path)).Element("root");
        }

        if (root == null)
        {
            Debug.Log("Level load failed");
            return;
        }
        GenerateScene(root);
    }

    private void GenerateScene(XElement root)
    {
        foreach (SaveableObject item in objects)
        {
            item.DestroySelf();
        }
        objects.Clear();

        foreach (XElement instance in root.Elements("instance"))
        {
            Vector3 position = Vector3.zero;

            position.x = float.Parse(instance.Attribute("x").Value, CultureInfo.InvariantCulture);
            position.y = float.Parse(instance.Attribute("y").Value, CultureInfo.InvariantCulture);
            position.z = float.Parse(instance.Attribute("z").Value, CultureInfo.InvariantCulture);

            Instantiate(Resources.Load<GameObject>(instance.Value), position, Quaternion.identity);
        }
    }

    //public void Save() {
    //    savedGames.Add(Game.Current);
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
    //    bf.Serialize(file, savedGames);
    //    file.Close();
    //    Debug.Log("Save");
    //}

    //public void Load() {
    //    if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
    //        savedGames = (List<Game>)bf.Deserialize(file);
    //        file.Close();
    //        Debug.Log("Load");
    //    }
    //}
}
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour {

    public List<SaveableObject> objects = new List<SaveableObject>();


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

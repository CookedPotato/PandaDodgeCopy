using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Recipe3Display : MonoBehaviour 
{
    public Image Picture;
    public Image Score;

    void Start() {
        Save savedData = readData();
        Picture.enabled = true;
        Score.enabled = true;


        
        if (savedData.unlocked[3] == false) {
            Picture.enabled = false;
            Score.enabled = false;
        }

    }


    public Save readData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream OldFile = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        Save OldData = (Save)bf.Deserialize(OldFile);
        OldFile.Close();

        return OldData;
    }
}
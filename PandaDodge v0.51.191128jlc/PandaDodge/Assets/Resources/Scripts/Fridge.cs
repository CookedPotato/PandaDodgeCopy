using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Fridge : MonoBehaviour
{
    public Image[] items = new Image[12];
    public GameObject[] ingredients = new GameObject[19]; //SUBJECT TO CHANGE FOR FUTURE INGRIDIENTS
    public Dictionary<String, GameObject> ingredientTable = new Dictionary<String, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientTable.Add(ingredients[i].name, ingredients[i]);
        }
        showItems();
    }

    public void showItems()
    {
        Save myData = readData();
        Dictionary<string, int> ingredientsToShow = myData.ingredientsCollected;
        int numItem = ingredientsToShow.Count;
        int maxNumOfIteration = 0;
        maxNumOfIteration = numItem > items.Length ? items.Length : numItem;
        int numInteration = 0;
        if (numItem > 0)
        {
            foreach (KeyValuePair<string, int> thisIngredient in ingredientsToShow)
            {
                if (numInteration < maxNumOfIteration)
                {
                    GameObject thisObject = ingredientTable[thisIngredient.Key];
                    items[numInteration].sprite = thisObject.GetComponent<SpriteRenderer>().sprite;
                    items[numInteration].GetComponent<Image>().enabled = true;
                    Text thisChild = items[numInteration].GetComponentsInChildren<Text>()[0];
                    thisChild.text = thisIngredient.Value.ToString();
                    thisChild.GetComponent<Text>().enabled = true;
                    numInteration += 1;
                }
                else
                {
                    break;
                }
            }
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

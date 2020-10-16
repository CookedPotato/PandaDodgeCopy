using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class StoreControl : MonoBehaviour
{

	int moneyAmount;
	public Text moneyAmountText;
	public Text heartPriceText;
	int heartPrice;
	public Button buyHeartButton;
	public Dictionary<string, int> ingredients;

    // Start is called before the first frame update
    void Start()
    {	
		Save data = readData();
		moneyAmount = data.coins;
		heartPrice = Int32.Parse(heartPriceText.text);

    }

    // Update is called once per frame
    void Update()
    {
        moneyAmountText.text = moneyAmount.ToString();
		if (moneyAmount >= heartPrice){
			buyHeartButton.interactable = true;
			//TODO: Add one more live to the play stage
		}else{
			buyHeartButton.interactable = false;
		}
    }

	public void buyHeart(){
		Save data = readData();
		Debug.Log("Click to Buy Heart");
        Analytics analytics = new Analytics();
        analytics.UpdateCoinUsage(heartPrice);
        analytics.UpdateHeartUsage();
		moneyAmount -= heartPrice;
		heartPriceText.text = "Sold!";
		buyHeartButton.gameObject.SetActive(false);
		updateCoin(moneyAmount);
	}

	public Save readData(){
        BinaryFormatter bf = new BinaryFormatter();
	    FileStream OldFile = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
		Save OldData = (Save)bf.Deserialize(OldFile);
        OldFile.Close();
		return OldData;
	}

	public void updateCoin(int coins){
		Save save = CreateSaveGameObject();
		BinaryFormatter bf = new BinaryFormatter();
	    FileStream OldFile = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
		Save OldData = (Save)bf.Deserialize(OldFile);
        OldFile.Close();

		save.ingredientsCollected = OldData.ingredientsCollected;
		save.coins = coins;
		save.levelCompleted = OldData.levelCompleted;

		FileStream NewFile = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(NewFile, save);
        NewFile.Close();

	}

	private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.ingredientsCollected = ingredients;
        save.coins = 0;
        save.levelCompleted = 0;

        return save;
    }
}

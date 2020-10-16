using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelControl : MonoBehaviour
{
    public GameObject note;
    public Button[] levels;
    public Text noteText;
	public GameObject displayInfoL1;
	public GameObject displayInfoL2;
	public GameObject displayInfoL3;
	public GameObject displayInfoL4;
	public GameObject displayInfoL5;

	public bool unlockl1;
	public bool unlockl2;
	public bool unlockl3;
	public bool unlockl4;
	public bool unlockl5;
    
    SceneSwitcher ss;
    // Start is called before the first frame update
    void Start()
    {
        Save savedData = readData();
		unlockl1 = true;
		unlockl2 = savedData.unlocked[1];
		unlockl3 = savedData.unlocked[2];
		unlockl4 = savedData.unlocked[3];
		unlockl5 = savedData.unlocked[4];

		Debug.Log("weqwwq");
		Debug.Log(unlockl1);
		Debug.Log(unlockl2);
		Debug.Log(unlockl3);
		Debug.Log(unlockl4);
		Debug.Log(unlockl5);

        for (int i = 0; i < levels.Length; i++)
        {
            if (savedData.unlocked[i])
            {
                Analytics analytics = new Analytics();
                analytics.UpdateHighestLevel(i + 1);
				if (i == 0){
					levels[0].GetComponent<Button>().onClick.AddListener(showInfoL1);
				}
				if (i == 1){
					levels[1].GetComponent<Button>().onClick.AddListener(showInfoL2);
				}
				if (i == 2){
					levels[2].GetComponent<Button>().onClick.AddListener(showInfoL3);
				}
				if (i == 3){
					levels[3].GetComponent<Button>().onClick.AddListener(showInfoL4);
				}
				if (i == 4){
					levels[4].GetComponent<Button>().onClick.AddListener(showInfoL5);
				}

			}
            else
            {
                if (i == 1) 
                {
                    levels[i].GetComponent<Button>().onClick.AddListener(alert1);
                }
                if (i == 2) 
                {
                    levels[i].GetComponent<Button>().onClick.AddListener(alert2);
                }
                if (i == 3)
                {
                    levels[i].GetComponent<Button>().onClick.AddListener(alert3);
                }
                if (i == 4)
                {
                    levels[i].GetComponent<Button>().onClick.AddListener(alert4);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void showInfoL1()
	{
		Debug.Log("Show Info l1");
		if (unlockl1){
			displayInfoL1.SetActive(true);
		}
	}

	public void closeInfoL1(){
		displayInfoL1.SetActive(false);
	}

	public void showInfoL2()
	{
		Debug.Log("Show Info l2");
		if (unlockl2){
			displayInfoL2.SetActive(true);
		}
	}

	public void closeInfoL2(){
		displayInfoL2.SetActive(false);
	}

	public void showInfoL3()
	{
		Debug.Log("Show Info l3");
		if (unlockl3){
			displayInfoL3.SetActive(true);
		}
	}

	public void closeInfoL3(){
		displayInfoL3.SetActive(false);
	}

	public void showInfoL4()
	{
		Debug.Log("Show Info l4");
		if (unlockl4){
			displayInfoL4.SetActive(true);
		}
	}

	public void closeInfoL4(){
		displayInfoL4.SetActive(false);
	}

	public void showInfoL5()
	{
		Debug.Log("Show Info l5");
		if (unlockl5){
			displayInfoL5.SetActive(true);
		}
	}

	public void closeInfoL5(){
		displayInfoL5.SetActive(false);
	}



    public void switchScene()
    {
        ss = levels[0].GetComponent<SceneSwitcher>();
        ss.PlayGame();
    }

    public void alert1() 
    {
        Save savedData = readData();
        if (!savedData.unlocked[1])
        {
            noteText.text = "You shall not pass!\n Uhhhhhh ------  I want to eat\n STEAMED EGGPLANT (蒜蓉蒸茄子). \n_(:зゝ∠)_.";
        }
        noteText.color = Color.white;
        note.SetActive(true);
        
    }


    public void alert2() 
    {
        Save savedData = readData();
        if (savedData.unlocked[1]==false && savedData.unlocked[2]==true)
        {
            noteText.text = "Level 2 needs to be completed to access the unlocked level 3 !";
        }
        else
        {
            noteText.text = "You shall not pass!\n Uhhhhhh ------  I want to eat\n fried MUSHROOM & GREEN PEPPER WITH diced ONION (洋葱炒蘑菇青椒). \n_(:зゝ∠)_.";
        }
               

        noteText.color = Color.white;
        note.SetActive(true);
    }

    public void alert3()
    {
        Save savedData = readData();
        if (!savedData.unlocked[1] || !savedData.unlocked[2] || !savedData.unlocked[3])
        {
            noteText.text = "You shall not pass!\n Uhhhhhh ------  I want to eat\n STEAMED Lotus root, Pumpkin and Green pepper(蒸莲藕南瓜和青椒). \n_(:зゝ∠)_.";
        }
        noteText.color = Color.white;
        note.SetActive(true);

    }

    public void alert4()
    {
        Save savedData = readData();
        if (!savedData.unlocked[1] || !savedData.unlocked[2] || !savedData.unlocked[3] || !savedData.unlocked[4])
        {
            noteText.text = "You shall not pass!\n Uhhhhhh ------  I want to eat\n steamed green beans, broccoli, and peach(蒸绿豆，花菜和桃子). \n_(:зゝ∠)_.";
        }
        noteText.color = Color.white;
        note.SetActive(true);

    }


    public void close()
    {
        note.SetActive(false);
    }

    public Save readData()
    {
        BinaryFormatter bf = new BinaryFormatter();

        Save OldData = new Save();

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream OldFile = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            OldData = (Save)bf.Deserialize(OldFile);
            OldFile.Close();
        } else
        {
            FileStream NewFile = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(NewFile, OldData);
            NewFile.Close();
        }
        
        

        

        return OldData;
    }

    public void saveAfterCraft(Save save) 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream NewFile = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(NewFile, save);
        NewFile.Close();

        Debug.Log("Game Saved Level Control");
    }


}

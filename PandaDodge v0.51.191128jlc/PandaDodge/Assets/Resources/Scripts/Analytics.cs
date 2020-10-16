using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Analytics : MonoBehaviour
{
    private string SAVE_PATH = System.Environment.CurrentDirectory + "/Assets/Analytics/";

    private class AnalyticData
    {
        /*
         *1.How many players goes to cook the dish after finishing catching the materials( Find if the flow is understood by players and if the cooking part is interesting for players).
         */
        public int kitchenClicked;          // When summary toKitchen clicked += 1
        public int totalGame;               // When game start += 1

        /*
         *3.How many players cooked the dish correctly after having the right ingredients in the fridge(Find the difficulty of figuring out the dish).
         */
        public int cookClicked;             // When cook in kitchen clicked += 1
        public int successCook;             // When cook successful += 1

        /*
         *4. Number of levels a player has passed(vip players).
         */
        public int highestLevel;             // When unlock new level += 1

        /*
         *5. How many people leave this game after playing each level.(find levels that are not so popular).
         */
        public int lastLevelPlayed;         // When game start record level num

        /*
         *6. Number of players complete each level(together with #2, to find the reason players leave this game).
         */
        public int[] levelCompleted;        // Array length = level num, index + 1 = level, value = completion

        /*
         *7. How many times a player restart each level to collect all the materials for cooking the dish of the level(Find the difficulty of the level).
         */
        public int[] restartNumber;         // Array length = level num, index + 1 = level, value = restart. When completed is fales, start game += 1; 

        /*
         *8. Number of materials the player catches(what materials users are more likely to catch)
         */
        public int[] lv1;
        public int[] lv2;                   //int [0.avocado, 1.roccoli, 2.broccoli2, 3.carrot, 4.chineseOnion, 5.eggplant, 6.garlic, 7.greenBeans, 8.greenPepper, 9.lotusRoot,
        public int[] lv3;
        public int[] lv4;
        public int[] lv5;
        //     10.maize = 0, 11.mushroom, 12.onion, 13.peach, 14.pepper, 15.pumpkin, 16.purplePotato, 17.redPepper, 18.tomato, 19. yellowPepper;

        /*
         *9. How many times a player uses tools and what tool they buy(find the popularity of tools).
         */
        public int heartUsage;              // When buy += 1

        /*
         *10.How many coins players get per level.How many coins they spend per level.
         */
        public int[] coinUsage;             // Array length = level num, index + 1 = level, value = restart. When completed is fales, buy += amount;                
    }

    // Called before fetch local data to ensure file exists
    public void CheckSave()
    {
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        if (!File.Exists(SAVE_PATH + "/analytics.txt"))
        {
            AnalyticData newData = new AnalyticData
            {
                kitchenClicked = 0,
                totalGame = 0,
                cookClicked = 0,
                successCook = 0,
                highestLevel = 1,
                lastLevelPlayed = 1,
                levelCompleted = new int[] { 0, 0, 0, 0, 0 },
                restartNumber = new int[] { 0, 0, 0, 0, 0 },
                lv1 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                lv2 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                lv3 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                lv4 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                lv5 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                heartUsage = 0,
                coinUsage = new int[] { 0, 0, 0, 0, 0 },
            };

            FileStream NewFile = File.Create(Application.persistentDataPath + "/analytics.txt");
            string json = JsonUtility.ToJson(newData);
            Save(json);
        }
    }

    public void Save (string saveString)
    {
        File.WriteAllText(SAVE_PATH + "/analytics.txt", saveString);
    }

    public string Load()
    {
        string saveString = File.ReadAllText(SAVE_PATH + "/analytics.txt");
        return saveString;
    }

    public void UpdateKitchClicked()
    {
        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        analyticData.kitchenClicked += 1;
        string json = JsonUtility.ToJson(analyticData);
        Save(json);
    }

    public void UpdateTotalGame()
    {
        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        analyticData.totalGame += 1;
        string json = JsonUtility.ToJson(analyticData);
        Save(json);
    }

    public void UpdateCookClicked()
    {
        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        analyticData.cookClicked += 1;
        string json = JsonUtility.ToJson(analyticData);
        Save(json);
    }

    public void UpdateSuccessCook()
    {
        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        analyticData.successCook += 1;
        string json = JsonUtility.ToJson(analyticData);
        Save(json);
    }
    
    public void UpdateHighestLevel(int level)
    {
        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        analyticData.highestLevel = level;
        string json = JsonUtility.ToJson(analyticData);
        Save(json);
    }

    public void Lv1licked()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save data = (Save)bf.Deserialize(file);
            file.Close();

            if (data.unlocked[0])
            {
                CheckSave();
                string saveString = Load();
                AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
                analyticData.lastLevelPlayed = 1;
                string json = JsonUtility.ToJson(analyticData);
                Save(json);
            }
        }   
    }

    public void Lv2licked()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save data = (Save)bf.Deserialize(file);
            file.Close();

            if (data.unlocked[1])
            {
                CheckSave();
                string saveString = Load();
                AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
                analyticData.lastLevelPlayed = 2;
                string json = JsonUtility.ToJson(analyticData);
                Save(json);
            }
        }
    }

    public void Lv3licked()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save data = (Save)bf.Deserialize(file);
            file.Close();

            if (data.unlocked[2])
            {
                CheckSave();
                string saveString = Load();
                AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
                analyticData.lastLevelPlayed = 3;
                string json = JsonUtility.ToJson(analyticData);
                Save(json);
            }
        }
    }
    public void Lv4licked()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save data = (Save)bf.Deserialize(file);
            file.Close();

            if (data.unlocked[3])
            {
                CheckSave();
                string saveString = Load();
                AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
                analyticData.lastLevelPlayed = 4;
                string json = JsonUtility.ToJson(analyticData);
                Save(json);
            }
        }
    }
    public void Lv5licked()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save data = (Save)bf.Deserialize(file);
            file.Close();

            if (data.unlocked[4])
            {
                CheckSave();
                string saveString = Load();
                AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
                analyticData.lastLevelPlayed = 5;
                string json = JsonUtility.ToJson(analyticData);
                Save(json);
            }
        }
    }

    public void UpdateLevelCompleted()
    {

        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        int lvlPlayed = analyticData.lastLevelPlayed;
        analyticData.levelCompleted[lvlPlayed - 1]++;
        string json = JsonUtility.ToJson(analyticData);
        Save(json);

    }

    public void UpdateRestartNumber()
    {
        int nextLevel = 0;
        int dataLength = 0;
        BinaryFormatter bf = new BinaryFormatter();
        Save data;
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            data = (Save)bf.Deserialize(file);
            file.Close();

            dataLength = data.unlocked.Length; // = 5

            CheckSave();
            string saveString = Load();
            AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
            int lvlPlayed = analyticData.lastLevelPlayed;
            nextLevel = analyticData.lastLevelPlayed + 1 > dataLength ? dataLength : analyticData.lastLevelPlayed;
            if (!data.unlocked[nextLevel - 1])
            {
                analyticData.restartNumber[nextLevel-2]++;
            }
            string json = JsonUtility.ToJson(analyticData);
            Save(json);
        }
    }

    public void UpdateLv(KeyValuePair<string, int> ingredient)
    {
        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        int lvlPlayed = analyticData.lastLevelPlayed;


        switch (analyticData.lastLevelPlayed)
        {
            case 1:
                switch (ingredient.Key)
                {
                    //    [0.avocado, 1.broccoli, 2.broccoli2, 3.carrot, 4.chineseOnion, 5.eggplant, 6.garlic, 7.greenBeans, 8.greenPepper, 9.lotusRoot,
                    //     10.maize, 11.mushroom, 12.onion, 13.peach, 14.pepper, 15.pumpkin, 16.purplePotato, 17.redPepper, 18.tomato, 19. yellowPepper];
                    case "Avocado":
                        analyticData.lv1[0] += ingredient.Value;
                        break;
                    case "Broccoli":
                        analyticData.lv1[1] += ingredient.Value;
                        break;
                    case "Broccoli2":
                        analyticData.lv1[2] += ingredient.Value;
                        break;
                    case "Carrot":
                        analyticData.lv1[3] += ingredient.Value;
                        break;
                    case "Eggplant":
                        analyticData.lv1[4] += ingredient.Value;
                        break;
                    case "Chinese onion":
                        analyticData.lv1[5] += ingredient.Value;
                        break;
                    case "Garlic":
                        analyticData.lv1[6] += ingredient.Value;
                        break;
                    case "Green beans":
                        analyticData.lv1[7] += ingredient.Value;
                        break;
                    case "Green pepper":
                        analyticData.lv1[8] += ingredient.Value;
                        break;
                    case "Lotus root":
                        analyticData.lv1[9] += ingredient.Value;
                        break;
                    case "Maize":
                        analyticData.lv1[10] += ingredient.Value;
                        break;
                    case "Mushroom":
                        analyticData.lv1[11] += ingredient.Value;
                        break;
                    case "Onion":
                        analyticData.lv1[12] += ingredient.Value;
                        break;
                    case "Peach":
                        analyticData.lv1[13] += ingredient.Value;
                        break;
                    case "Pepper":
                        analyticData.lv1[14] += ingredient.Value;
                        break;
                    case "Pumpkin":
                        analyticData.lv1[15] += ingredient.Value;
                        break;
                    case "Purple potato":
                        analyticData.lv1[16] += ingredient.Value;
                        break;
                    case "Red pepper":
                        analyticData.lv1[17] += ingredient.Value;
                        break;
                    case "Tomato":
                        analyticData.lv1[18] += ingredient.Value;
                        break;
                    case "Yellow pepper":
                        analyticData.lv1[19] += ingredient.Value;
                        break;
                }
                break;
            case 2:
                switch (ingredient.Key)
                {
                    //    [0.avocado, 1.broccoli, 2.broccoli2, 3.carrot, 4.chineseOnion, 5.eggplant, 6.garlic, 7.greenBeans, 8.greenPepper, 9.lotusRoot,
                    //     10.maize, 11.mushroom, 12.onion, 13.peach, 14.pepper, 15.pumpkin, 16.purplePotato, 17.redPepper, 18.tomato, 19. yellowPepper];
                    case "Avocado":
                        analyticData.lv2[0] += ingredient.Value;
                        break;
                    case "Broccoli":
                        analyticData.lv2[1] += ingredient.Value;
                        break;
                    case "Broccoli2":
                        analyticData.lv2[2] += ingredient.Value;
                        break;
                    case "Carrot":
                        analyticData.lv2[3] += ingredient.Value;
                        break;
                    case "Eggplant":
                        analyticData.lv2[4] += ingredient.Value;
                        break;
                    case "Chinese onion":
                        analyticData.lv2[5] += ingredient.Value;
                        break;
                    case "Garlic":
                        analyticData.lv2[6] += ingredient.Value;
                        break;
                    case "Green beans":
                        analyticData.lv2[7] += ingredient.Value;
                        break;
                    case "Green pepper":
                        analyticData.lv2[8] += ingredient.Value;
                        break;
                    case "Lotus root":
                        analyticData.lv2[9] += ingredient.Value;
                        break;
                    case "Maize":
                        analyticData.lv2[10] += ingredient.Value;
                        break;
                    case "Mushroom":
                        analyticData.lv2[11] += ingredient.Value;
                        break;
                    case "Onion":
                        analyticData.lv2[12] += ingredient.Value;
                        break;
                    case "Peach":
                        analyticData.lv2[13] += ingredient.Value;
                        break;
                    case "Pepper":
                        analyticData.lv2[14] += ingredient.Value;
                        break;
                    case "Pumpkin":
                        analyticData.lv2[15] += ingredient.Value;
                        break;
                    case "Purple potato":
                        analyticData.lv2[16] += ingredient.Value;
                        break;
                    case "Red pepper":
                        analyticData.lv2[17] += ingredient.Value;
                        break;
                    case "Tomato":
                        analyticData.lv2[18] += ingredient.Value;
                        break;
                    case "Yellow pepper":
                        analyticData.lv2[19] += ingredient.Value;
                        break;
                }
                break;
            case 3:
                switch (ingredient.Key)
                {
                    //    [0.avocado, 1.broccoli, 2.broccoli2, 3.carrot, 4.chineseOnion, 5.eggplant, 6.garlic, 7.greenBeans, 8.greenPepper, 9.lotusRoot,
                    //     10.maize, 11.mushroom, 12.onion, 13.peach, 14.pepper, 15.pumpkin, 16.purplePotato, 17.redPepper, 18.tomato, 19. yellowPepper];
                    case "Avocado":
                        analyticData.lv3[0] += ingredient.Value;
                        break;
                    case "Broccoli":
                        analyticData.lv3[1] += ingredient.Value;
                        break;
                    case "Broccoli2":
                        analyticData.lv3[2] += ingredient.Value;
                        break;
                    case "Carrot":
                        analyticData.lv3[3] += ingredient.Value;
                        break;
                    case "Eggplant":
                        analyticData.lv3[4] += ingredient.Value;
                        break;
                    case "Chinese onion":
                        analyticData.lv3[5] += ingredient.Value;
                        break;
                    case "Garlic":
                        analyticData.lv3[6] += ingredient.Value;
                        break;
                    case "Green beans":
                        analyticData.lv3[7] += ingredient.Value;
                        break;
                    case "Green pepper":
                        analyticData.lv3[8] += ingredient.Value;
                        break;
                    case "Lotus root":
                        analyticData.lv3[9] += ingredient.Value;
                        break;
                    case "Maize":
                        analyticData.lv3[10] += ingredient.Value;
                        break;
                    case "Mushroom":
                        analyticData.lv3[11] += ingredient.Value;
                        break;
                    case "Onion":
                        analyticData.lv3[12] += ingredient.Value;
                        break;
                    case "Peach":
                        analyticData.lv3[13] += ingredient.Value;
                        break;
                    case "Pepper":
                        analyticData.lv3[14] += ingredient.Value;
                        break;
                    case "Pumpkin":
                        analyticData.lv3[15] += ingredient.Value;
                        break;
                    case "Purple potato":
                        analyticData.lv3[16] += ingredient.Value;
                        break;
                    case "Red pepper":
                        analyticData.lv3[17] += ingredient.Value;
                        break;
                    case "Tomato":
                        analyticData.lv3[18] += ingredient.Value;
                        break;
                    case "Yellow pepper":
                        analyticData.lv3[19] += ingredient.Value;
                        break;
                }
                break;
            case 4:
                switch (ingredient.Key)
                {
                    //    [0.avocado, 1.broccoli, 2.broccoli2, 3.carrot, 4.chineseOnion, 5.eggplant, 6.garlic, 7.greenBeans, 8.greenPepper, 9.lotusRoot,
                    //     10.maize, 11.mushroom, 12.onion, 13.peach, 14.pepper, 15.pumpkin, 16.purplePotato, 17.redPepper, 18.tomato, 19. yellowPepper];
                    case "Avocado":
                        analyticData.lv4[0] += ingredient.Value;
                        break;
                    case "Broccoli":
                        analyticData.lv4[1] += ingredient.Value;
                        break;
                    case "Broccoli2":
                        analyticData.lv4[2] += ingredient.Value;
                        break;
                    case "Carrot":
                        analyticData.lv4[3] += ingredient.Value;
                        break;
                    case "Eggplant":
                        analyticData.lv4[4] += ingredient.Value;
                        break;
                    case "Chinese onion":
                        analyticData.lv4[5] += ingredient.Value;
                        break;
                    case "Garlic":
                        analyticData.lv4[6] += ingredient.Value;
                        break;
                    case "Green beans":
                        analyticData.lv4[7] += ingredient.Value;
                        break;
                    case "Green pepper":
                        analyticData.lv4[8] += ingredient.Value;
                        break;
                    case "Lotus root":
                        analyticData.lv4[9] += ingredient.Value;
                        break;
                    case "Maize":
                        analyticData.lv4[10] += ingredient.Value;
                        break;
                    case "Mushroom":
                        analyticData.lv4[11] += ingredient.Value;
                        break;
                    case "Onion":
                        analyticData.lv4[12] += ingredient.Value;
                        break;
                    case "Peach":
                        analyticData.lv4[13] += ingredient.Value;
                        break;
                    case "Pepper":
                        analyticData.lv4[14] += ingredient.Value;
                        break;
                    case "Pumpkin":
                        analyticData.lv4[15] += ingredient.Value;
                        break;
                    case "Purple potato":
                        analyticData.lv4[16] += ingredient.Value;
                        break;
                    case "Red pepper":
                        analyticData.lv4[17] += ingredient.Value;
                        break;
                    case "Tomato":
                        analyticData.lv4[18] += ingredient.Value;
                        break;
                    case "Yellow pepper":
                        analyticData.lv4[19] += ingredient.Value;
                        break;
                }
                break;
            case 5:
                switch (ingredient.Key)
                {
                    //    [0.avocado, 1.broccoli, 2.broccoli2, 3.carrot, 4.chineseOnion, 5.eggplant, 6.garlic, 7.greenBeans, 8.greenPepper, 9.lotusRoot,
                    //     10.maize, 11.mushroom, 12.onion, 13.peach, 14.pepper, 15.pumpkin, 16.purplePotato, 17.redPepper, 18.tomato, 19. yellowPepper];
                    case "Avocado":
                        analyticData.lv5[0] += ingredient.Value;
                        break;
                    case "Broccoli":
                        analyticData.lv5[1] += ingredient.Value;
                        break;
                    case "Broccoli2":
                        analyticData.lv5[2] += ingredient.Value;
                        break;
                    case "Carrot":
                        analyticData.lv5[3] += ingredient.Value;
                        break;
                    case "Eggplant":
                        analyticData.lv5[4] += ingredient.Value;
                        break;
                    case "Chinese onion":
                        analyticData.lv5[5] += ingredient.Value;
                        break;
                    case "Garlic":
                        analyticData.lv5[6] += ingredient.Value;
                        break;
                    case "Green beans":
                        analyticData.lv5[7] += ingredient.Value;
                        break;
                    case "Green pepper":
                        analyticData.lv5[8] += ingredient.Value;
                        break;
                    case "Lotus root":
                        analyticData.lv5[9] += ingredient.Value;
                        break;
                    case "Maize":
                        analyticData.lv5[10] += ingredient.Value;
                        break;
                    case "Mushroom":
                        analyticData.lv5[11] += ingredient.Value;
                        break;
                    case "Onion":
                        analyticData.lv5[12] += ingredient.Value;
                        break;
                    case "Peach":
                        analyticData.lv5[13] += ingredient.Value;
                        break;
                    case "Pepper":
                        analyticData.lv5[14] += ingredient.Value;
                        break;
                    case "Pumpkin":
                        analyticData.lv5[15] += ingredient.Value;
                        break;
                    case "Purple potato":
                        analyticData.lv5[16] += ingredient.Value;
                        break;
                    case "Red pepper":
                        analyticData.lv5[17] += ingredient.Value;
                        break;
                    case "Tomato":
                        analyticData.lv5[18] += ingredient.Value;
                        break;
                    case "Yellow pepper":
                        analyticData.lv5[19] += ingredient.Value;
                        break;
                }
                break;
        }
        string json = JsonUtility.ToJson(analyticData);
        Save(json);
    }

    public void UpdateHeartUsage()
    {
        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        analyticData.heartUsage++;
        string json = JsonUtility.ToJson(analyticData);
        Save(json);
    }

    public void UpdateCoinUsage(int amount)
    {
        int nextLevel = 0;
        int dataLength = 0;
        BinaryFormatter bf = new BinaryFormatter();
        Save data;
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            data = (Save)bf.Deserialize(file);
            file.Close();

            dataLength = data.unlocked.Length;

            CheckSave();
            string saveString = Load();
            AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
            int lvlPlayed = analyticData.lastLevelPlayed;
            nextLevel = analyticData.lastLevelPlayed + 1 > dataLength ? dataLength : analyticData.lastLevelPlayed;
            if (!data.unlocked[nextLevel - 1])
            {
                analyticData.coinUsage[nextLevel - 2] += amount;
            }
            string json = JsonUtility.ToJson(analyticData);
            Save(json);
        }
    }

    public int ReturnLastLevel()
    {
        CheckSave();
        string saveString = Load();
        AnalyticData analyticData = JsonUtility.FromJson<AnalyticData>(saveString);
        int lvlPlayed = analyticData.lastLevelPlayed;
        return lvlPlayed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CraftControl : MonoBehaviour
{
	public Text recipeDisplay;

    public Dropdown ingredient1;
    public Dropdown ingredient2;
    public Dropdown ingredient3;
	public Dropdown ingredient4;
	public Dropdown ingredient5;

    public Text textField1;
    public Text textField2;
    public Button toMap;

	public GameObject notificationPanel;
    public GameObject craftResultPanel;
    public GameObject[] ingredients = new GameObject[19]; //SUBJECT TO CHANGE FOR FUTURE INGRIDIENTS
    public Dictionary<String, GameObject> ingredientTable = new Dictionary<String, GameObject>();

	// Images for dishes
	public Sprite noDish;
	public Sprite level1Dish;
	public Sprite level2Dish;
	public Sprite level3Dish;
	public Sprite level4Dish;
	public Sprite level5Dish;

	// Images for dish levels
	public Sprite star0;
	public Sprite star1;
	public Sprite star2;
	public Sprite star3;

	public Image dishImage;
	public Image starImage;

    private List<String> Recipe_1 = new List<String> { "Eggplant", "Garlic", "Sharp pepper" };         // My Secret Recipe_1
    private List<String> Recipe_2 = new List<String> { "Mushroom", "Onion", "Green pepper" };          // My Secret Recipe_2
    private List<String> Recipe_3 = new List<String> { "Lobster", "Chinese onion", "Sharp pepper" };   // My Secret Recipe_3
    private List<String> Recipe_4 = new List<String> { "Broccoli", "Carrot", "Mushroom" };             // My Secret Recipe_4
	private List<String> Recipe_5 = new List<String> { "Avocado", "Pumpkin", "Green beans" };          // My Secret Recipe_5

	private List<String> Recipe_1_bonus = new List<String> { "Chinese onion", "Coriander" };           // My Secret Recipe_1 bonus
    private List<String> Recipe_2_bonus = new List<String> { "Garlic", "Pork" };                       // My Secret Recipe_2 bonus
    private List<String> Recipe_3_bonus = new List<String> { "Garlic", "Coriander" };                  // My Secret Recipe_3 bonus
    private List<String> Recipe_4_bonus = new List<String> { "Garlic", "Pork" };                       // My Secret Recipe_4 bonus
	private List<String> Recipe_5_bonus = new List<String> { "Shrimp", "Garlic" };                     // My Secret Recipe_5 bonus

    void Start()
    {
        ingredient1.ClearOptions();
        ingredient2.ClearOptions();
        ingredient3.ClearOptions();
		ingredient4.ClearOptions();
		ingredient5.ClearOptions();

        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientTable.Add(ingredients[i].name, ingredients[i]);
        }

       
        Save savedData = readData();
        Dictionary<string, int> ingredientsToShow = savedData.ingredientsCollected;

        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
		optionDatas.Add(new Dropdown.OptionData("Select", null));
        //List<string> optionDatas = new List<string>();
        foreach (KeyValuePair<string, int> thisIngredient in ingredientsToShow) {
            string myKey = thisIngredient.Key;
            GameObject myObj = ingredientTable[myKey];
            Sprite mySprite = myObj.GetComponent<SpriteRenderer>().sprite;
            var dataOption = new Dropdown.OptionData(myKey, mySprite);
            optionDatas.Add(dataOption);
        }
        //Debug.Log(optionDatas.Count);
        //for (int i = 0; i < optionDatas.Count; i++)
        //{
         //   Debug.Log(optionDatas[i].GetType());
        //}

        ingredient1.AddOptions(optionDatas);
        ingredient2.AddOptions(optionDatas);
        ingredient3.AddOptions(optionDatas);
		ingredient4.AddOptions(optionDatas);
		ingredient5.AddOptions(optionDatas);

		string recipe = "Recipe: ";
		if (!savedData.unlocked[1]) {
			recipe += String.Join(", ", Recipe_1.ToArray()) + ", " + String.Join(", ", Recipe_1_bonus.ToArray());
		} else if (!savedData.unlocked[2]) {
			recipe += String.Join(", ", Recipe_2.ToArray()) + ", " + String.Join(", ", Recipe_2_bonus.ToArray());
		} else if (!savedData.unlocked[3]) {
			recipe += String.Join(", ", Recipe_3.ToArray()) + ", " + String.Join(", ", Recipe_3_bonus.ToArray());
		} else if (!savedData.unlocked[4]) {
			recipe += String.Join(", ", Recipe_4.ToArray()) + ", " + String.Join(", ", Recipe_4_bonus.ToArray());
		} else if (!savedData.unlocked[5]) {
			recipe += String.Join(", ", Recipe_5.ToArray()) + ", " + String.Join(", ", Recipe_5_bonus.ToArray());
		} else {
			recipe = "Yeaa! You have finished all the dishes!";
			recipeDisplay.color = Color.red;
		}
		recipeDisplay.text = recipe;
    }

    public Save readData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream OldFile = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        Save OldData = (Save) bf.Deserialize(OldFile);
        OldFile.Close();

        return OldData;
    }

    public void craft()
    {
		// Required
        string option1 = ingredient1.options[ingredient1.value].text;
        string option2 = ingredient2.options[ingredient2.value].text;
        string option3 = ingredient3.options[ingredient3.value].text;

		// Optional
		string option4 = ingredient4.options[ingredient4.value].text;
		string option5 = ingredient5.options[ingredient5.value].text;

		// Step 1. Check if ingredients are enough
		if (option1.Equals("Select") || option2.Equals("Select") || option3.Equals("Select")) {
			notificationPanel.SetActive(true);
			return;
		}
		
		// Step 2. Load and update data
        Save savedData = readData();
        savedData.ingredientsCollected[option1] -= 1;
        if (savedData.ingredientsCollected[option1] == 0)
        {
            savedData.ingredientsCollected.Remove(option1);
        }
        savedData.ingredientsCollected[option2] -= 1;
        if (savedData.ingredientsCollected[option2] == 0)
        {
            savedData.ingredientsCollected.Remove(option2);
        }
        savedData.ingredientsCollected[option3] -= 1;
        if (savedData.ingredientsCollected[option3] == 0)
        {
            savedData.ingredientsCollected.Remove(option3);
        }
		if (!option4.Equals("Select")) {
			savedData.ingredientsCollected[option4] -= 1;
			if (savedData.ingredientsCollected[option4] == 0)
			{
				savedData.ingredientsCollected.Remove(option4);
			}
		}
		if (!option5.Equals("Select")) {
			savedData.ingredientsCollected[option5] -= 1;
			if (savedData.ingredientsCollected[option5] == 0)
			{
				savedData.ingredientsCollected.Remove(option5);
			}
		}

		// Step 3. Cook process checking
        // CODE BELOW IS SUBJECT TO CHANGE
        string head = "Bravo!";
        string myParagraph;
        if (!savedData.unlocked[1] && Recipe_1.Contains(option1) && Recipe_1.Contains(option2) && Recipe_1.Contains(option3))      // Flawed Logic. Change Later
        {
            savedData.unlocked[1] = true;
			dishImage.sprite = level1Dish;
			
			// Bonus checking
			if (Recipe_1_bonus.Contains(option4) && Recipe_1_bonus.Contains(option5)) {
				starImage.sprite = star3;
				savedData.stars[0] = 3;
			} else if (Recipe_1_bonus.Contains(option4) || Recipe_1_bonus.Contains(option5)) {
				starImage.sprite = star2;
				savedData.stars[0] = 2;
			} else {
				starImage.sprite = star1;
				savedData.stars[0] = 1;
			}

            // POP SUCCESS
            myParagraph = "Level 2 is unlocked!";
            Analytics analytics = new Analytics();
            analytics.UpdateSuccessCook();
        }
        else if (!savedData.unlocked[2] && Recipe_2.Contains(option1) && Recipe_2.Contains(option2) && Recipe_2.Contains(option3))
        {
            savedData.unlocked[2] = true;
			dishImage.sprite = level2Dish;
			
			// Bonus checking
			if (Recipe_2_bonus.Contains(option4) && Recipe_2_bonus.Contains(option5)) {
				starImage.sprite = star3;
				savedData.stars[1] = 3;
			} else if (Recipe_2_bonus.Contains(option4) || Recipe_2_bonus.Contains(option5)) {
				starImage.sprite = star2;
				savedData.stars[1] = 2;
			} else {
				starImage.sprite = star1;
				savedData.stars[1] = 1;
			}

            // POP SUCCESS
            myParagraph = "Level 3 is unlocked!";
            Analytics analytics = new Analytics();
            analytics.UpdateSuccessCook();
        }
        else if (!savedData.unlocked[3] && Recipe_3.Contains(option1) && Recipe_3.Contains(option2) && Recipe_3.Contains(option3))
        {
            savedData.unlocked[3] = true;
			dishImage.sprite = level3Dish;
			
			// Bonus checking
			if (Recipe_3_bonus.Contains(option4) && Recipe_3_bonus.Contains(option5)) {
				starImage.sprite = star3;
				savedData.stars[2] = 3;
			} else if (Recipe_3_bonus.Contains(option4) || Recipe_3_bonus.Contains(option5)) {
				starImage.sprite = star2;
				savedData.stars[2] = 2;
			} else {
				starImage.sprite = star1;
				savedData.stars[2] = 1;
			}

            // POP SUCCESS
            myParagraph = "Level 4 is unlocked!";
            Analytics analytics = new Analytics();
            analytics.UpdateSuccessCook();
        }
        else if (!savedData.unlocked[4] && Recipe_4.Contains(option1) && Recipe_4.Contains(option2) && Recipe_4.Contains(option3))
        {
            savedData.unlocked[4] = true;
			dishImage.sprite = level4Dish;
			
			// Bonus checking
			if (Recipe_4_bonus.Contains(option4) && Recipe_4_bonus.Contains(option5)) {
				starImage.sprite = star3;
				savedData.stars[3] = 3;
			} else if (Recipe_4_bonus.Contains(option4) || Recipe_4_bonus.Contains(option5)) {
				starImage.sprite = star2;
				savedData.stars[3] = 2;
			} else {
				starImage.sprite = star1;
				savedData.stars[3] = 1;
			}

            // POP SUCCESS
            myParagraph = "Level 5 is unlocked!";
            Analytics analytics = new Analytics();
            analytics.UpdateSuccessCook();
        }
		else if (!savedData.unlocked[5] && Recipe_5.Contains(option1) && Recipe_5.Contains(option2) && Recipe_5.Contains(option3))
        {
			savedData.unlocked[5] = true;
			dishImage.sprite = level5Dish;
			
			// Bonus checking
			if (Recipe_5_bonus.Contains(option4) && Recipe_5_bonus.Contains(option5)) {
				starImage.sprite = star3;
				savedData.stars[4] = 3;
			} else if (Recipe_5_bonus.Contains(option4) || Recipe_5_bonus.Contains(option5)) {
				starImage.sprite = star2;
				savedData.stars[4] = 2;
			} else {
				starImage.sprite = star1;
				savedData.stars[4] = 1;
			}

            // POP SUCCESS
            myParagraph = "You passed the game!";
            Analytics analytics = new Analytics();
            analytics.UpdateSuccessCook();
        } 
        else
        {
            // POP FAIL
			dishImage.sprite = noDish;
			starImage.sprite = star0;
            head = "Sigh...";
            myParagraph = "You lost your ingredients.";
            textField2.color = Color.red;
        }

        textField1.text = head;
        textField2.text = myParagraph;

        craftResultPanel.SetActive(true);

        saveAfterCraft(savedData);
		
    }

	public void closeNotification() {
		notificationPanel.SetActive(false);
	}

    public void close()
    {
        craftResultPanel.SetActive(false);
    }

    public void saveAfterCraft(Save save)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream NewFile = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(NewFile, save);
        NewFile.Close();

        Debug.Log("Game Saved Craft Control");
    }
}

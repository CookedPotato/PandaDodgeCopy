using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public Dictionary<string, int> ingredientsCollected;
    public int coins;
    public int levelCompleted;
    
    public bool[] unlocked;
	public int[] stars = {0, 0, 0, 0, 0};

    public Save()
    {
        ingredientsCollected = new Dictionary<string, int> { };
        coins = 0;
        levelCompleted = 0;
        unlocked = new bool[6] { true, false, false, false, false, false };
        stars = new int[5] { 0, 0, 0, 0, 0 };
    }

    public Dictionary<string, int> dicGet()
    {
        return ingredientsCollected;
    }
}


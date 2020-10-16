using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Background : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite backgroundImage;
    public Sprite backgroundImage2;
    public Sprite backgroundImage3;
    public Sprite backgroundImage4;
    public Sprite backgroundImage5;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/lv1background");

        Save savedData = readData();
        Debug.Log("coins: " + savedData.coins);
        Analytics analytics = new Analytics();
        int lastLevel = analytics.ReturnLastLevel();
        switch (lastLevel)
        {
            case 1:
                spriteRenderer.sprite = backgroundImage;
                break;
            case 2:
                spriteRenderer.sprite = backgroundImage2;
                break;
            case 3:
                spriteRenderer.sprite = backgroundImage3;
                break;
            case 4:
                spriteRenderer.sprite = backgroundImage4;
                break;
            case 5:
                spriteRenderer.sprite = backgroundImage5;
                break;

        }
        
        
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight / Screen.height * Screen.width;
        float width = screenWidth / spriteRenderer.sprite.bounds.size.x;
        float height = screenHeight / spriteRenderer.sprite.bounds.size.y;
        transform.localScale = new Vector3(width, height, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Save readData() 
    {
        BinaryFormatter bf = new BinaryFormatter();
        //if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        //{
            FileStream OldFile = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save OldData = (Save)bf.Deserialize(OldFile);
            OldFile.Close();
            return OldData;
        //}
        //return new Save();
    }
}

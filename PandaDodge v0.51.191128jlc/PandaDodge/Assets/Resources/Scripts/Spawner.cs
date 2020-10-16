using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] hazards;
    public GameObject[] hazards_2;
    public GameObject[] hazards_3;
    public GameObject[] hazards_4;
    public GameObject[] hazards_5;
    public GameObject[] ingredients;
	public GameObject coin;

    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public float minTimeBetweenSpawns;
    public float decrease;

    public GameObject player;

    public int foodindex1;
    public int foodindex2;


    // Update is called once per frame
    void Update()
    {
        Save savedData = readData();
        if (player != null)
        {
            if (timeBtwSpawns <= 0)
            {
				// Choose the spawn point.
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

				// The ratio of possibility of dropping attacks, ingredients and coins is 3:3:1.
                GameObject randomHazard = null;
				int isAttack = Random.Range(0, 14);
				Analytics analytics = new Analytics();
                int lvl = analytics.ReturnLastLevel();
				if (isAttack < 6)
                {
                    switch (lvl)
                    {
                        case 1:
							randomHazard = hazards[Random.Range(0, hazards.Length)];
                            break;
                        case 2:
							randomHazard = hazards_2[Random.Range(0, hazards_2.Length)];
                            break;
                        case 3:
                            randomHazard = hazards_3[Random.Range(0, hazards_3.Length)];
                            break;
                        case 4:
                            randomHazard = hazards_4[Random.Range(0, hazards_4.Length)];
                            break;
                        case 5:
                            randomHazard = hazards_5[Random.Range(0, hazards_5.Length)];
                            break;
                    }
                } 
				else if (isAttack < 12) {
					/* With random extracting ingredients
                    // Ratio for required ingredients, optional ingredients and non ingredients is 2:1:3
					if (isAttack < 8) {
						randomHazard = ingredients[Random.Range((lvl - 1) * 8, (lvl - 1) * 8 + 3)];
					} else if (isAttack < 9) {
						randomHazard = ingredients[Random.Range((lvl - 1) * 8 + 3, (lvl - 1) * 8 + 5)];
					} else {
						randomHazard = ingredients[Random.Range((lvl - 1) * 8 + 5, lvl * 8)];
					}
					*/

					// Without random extracting ingredients
					if (isAttack < 9) {
						randomHazard = ingredients[Random.Range((lvl - 1) * 8, (lvl - 1) * 8 + 3)];
					} else {
						randomHazard = ingredients[Random.Range((lvl - 1) * 8 + 3, (lvl - 1) * 8 + 5)];
					}
				}
				else {
					randomHazard = coin;
				}

                ////////////////////////////////////////
                //what if food is dropped more often:
                //if (isAttack < 1)
                //   randomHazard = hazards[Random.Range(0, hazards.Length)];
                //else if (1 <= isAttack && isAttack >= 5)
                //    //randomHazard = ingredients[Random.Range(0, ingredients.Length)];
                //    randomHazard = ingredients[Random.Range(foodindex1, foodindex2)];
                //else
                //    randomHazard = coin;
                //what if food is dropped more often
                /////////////////////////////////////////


                GameObject g = Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.identity);
                
                if (startTimeBtwSpawns > minTimeBetweenSpawns)
                {
                    startTimeBtwSpawns -= decrease;
                }

                timeBtwSpawns = startTimeBtwSpawns;

             }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
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

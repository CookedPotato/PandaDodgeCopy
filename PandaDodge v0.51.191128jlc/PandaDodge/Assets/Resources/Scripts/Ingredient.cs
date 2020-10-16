using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
	public float minSpeed;
    public float maxSpeed;

	float speed;

    Player playerScript;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

	void OnTriggerEnter2D(Collider2D hitObject)
    {

        if(hitObject.tag == "Player") {
			// Remove the "(Clone)" at the end of the ingredient's name. 
			string name = gameObject.name;
			name = name.Substring(0, name.Length - 7);
			if (name.Equals("heart"))
				playerScript.AddHealth();
			else
				playerScript.CollectIngredient(name);
			Destroy(gameObject);
        }

        if (hitObject.tag == "Ground") {
            // Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}

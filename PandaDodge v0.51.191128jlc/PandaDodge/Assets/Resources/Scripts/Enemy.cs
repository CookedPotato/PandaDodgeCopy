using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;

    float speed;

    Player playerScript;

    public int damage;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() //changed this funtion
    {

        Analytics analytics = new Analytics();
        int lvl = analytics.ReturnLastLevel();
        switch (lvl)
        {
            case 1:
                transform.position += Vector3.down * speed * Time.deltaTime;
                break;
            case 2:
                transform.Rotate(0f, 0, 5f);
                transform.position += Vector3.down * speed * Time.deltaTime;
                break;
            case 3:
                transform.position += Vector3.down * speed * Time.deltaTime;
                transform.position += Vector3.left * speed * Time.deltaTime / 2;
                break;
            case 4:
                transform.Rotate(0f, 0, 5f);
                transform.position += Vector3.down * speed * Time.deltaTime;
                transform.position += Vector3.right * speed * Time.deltaTime / 2;
                break;
            case 5:
                transform.position += Vector3.down * speed * Time.deltaTime;
                transform.position += Vector3.left * speed * Time.deltaTime / 2;
                break;
            default:
                transform.Rotate(0f, 0, 5f);
                transform.position += Vector3.down * speed * Time.deltaTime;
                transform.position += Vector3.left * speed * Time.deltaTime / 4;
                transform.position += Vector3.right * speed * Time.deltaTime;
                break;
        }

    }

    void OnTriggerEnter2D(Collider2D hitObject)
    {

        if(hitObject.tag == "Player") {
            playerScript.TakeDamage(damage);
            // Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (hitObject.tag == "Ground") {
            // Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }


}

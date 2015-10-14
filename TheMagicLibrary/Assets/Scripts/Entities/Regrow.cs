using UnityEngine;
using System.Collections;

public class Regrow : MonoBehaviour {

    public float respawnRate = 60f;

    private float respawnTimer;
    private GameObject player;

    void Start () {
        respawnTimer = 0;
        if(GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }
	}
	
	void Update () {

        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player.gameObject)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            respawnTimer = respawnRate;
        }

    }

}

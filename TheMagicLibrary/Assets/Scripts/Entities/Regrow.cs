using UnityEngine;
using System.Collections;

public class Regrow : MonoBehaviour {

    public GameObject player;
    public float respawnRate = 60f;

    private float respawnTimer;

	void Start () {
        respawnTimer = 0;
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

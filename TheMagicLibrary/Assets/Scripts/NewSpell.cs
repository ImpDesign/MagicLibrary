using UnityEngine;
using System.Collections;

public class NewSpell : MonoBehaviour {
    public GameObject text;
    private GameObject player;

    void Start ()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player.gameObject)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            text.SetActive(true);
        }

    }
}

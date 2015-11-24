using UnityEngine;
using System.Collections;

public class TeleportingBookshelf : MonoBehaviour {

    public Transform destination;
    public bool inPosition = false;
    public bool canTeleport = true;

    private GameObject player;
    private Vector3 teleportDestination;
    private Color color;
    private float fadeOutTimer = 0;
    private float fadeInTimer = 0;
    private bool started;


    void Start () {

        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }

        color = player.gameObject.GetComponent<SpriteRenderer>().color;
        teleportDestination = destination.gameObject.transform.position;
        teleportDestination.y += 2.25f;
    }
	

	void Update () {

        if((canTeleport && inPosition)||started)
        {
            started = true;
            player.gameObject.GetComponent<PlayerController>().isTeleporting = true;
            if(fadeOutTimer == 0)
            {
                StopAllCoroutines();
                StartCoroutine("FadeOutSequence");
                //destination.GetComponent<TeleportingBookshelf>().canTeleport = false;
            }

            fadeOutTimer += Time.deltaTime;

            if(fadeOutTimer > 1)
            {
                player.GetComponent<PlayerController>().canTeleport = false;
                player.transform.position = teleportDestination;
                if(fadeInTimer == 0)
                {
                    StopAllCoroutines();
                    StartCoroutine("FadeInSequence");
                }

                fadeInTimer += Time.deltaTime;
                if(fadeInTimer > 1)
                {
                    StopAllCoroutines();
                    started = false;
                    fadeInTimer = 0;
                    fadeOutTimer = 0;
                    player.gameObject.GetComponent<PlayerController>().isTeleporting = false;
                }
            }
        }

        inPosition = false;
	
	}

    public float GetOutTimer
    {
        get
        {
            return fadeOutTimer;
        }
    }
    public float GetInTimer
    {
        get
        {
            return fadeInTimer;
        }
    }

    IEnumerator FadeOutSequence()
    {
        while (color.a > 0)
        {
            player.gameObject.GetComponent<SpriteRenderer>().color = color;
            color.a -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeInSequence()
    {
        color.a = 0;
        while (color.a < 255)
        {
            player.gameObject.GetComponent<SpriteRenderer>().color = color;
            color.a += Time.deltaTime;
            yield return null;
        }
    }
}

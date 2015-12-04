using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
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
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
}

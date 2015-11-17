using UnityEngine;
using System.Collections;

public class BackgroundMovement : MonoBehaviour {

    public float scrollSpeed = .1f;
    public float tileSizeZ;
    private GameObject player;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }
    }

    void Update()
    {
        float newPosition = Mathf.Repeat((player.transform.position.x * scrollSpeed)%1, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }

}

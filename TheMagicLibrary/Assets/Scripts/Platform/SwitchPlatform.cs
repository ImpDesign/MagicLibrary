using UnityEngine;
using System.Collections;

public class SwitchPlatform : MonoBehaviour {

    public GameObject door;
    public bool active = false;
    public bool isUsed = false;
    public bool cameraActive = false;

    private float speed = 2;
    private float timer = 0;
    private float timer2 = 0;
    private float timer3 = 0;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    private GameObject player;

    void Start()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }

        startPosition = gameObject.transform.position;
        endPosition = startPosition;
        endPosition.y -= 2f;

        float distance = Vector3.Distance(startPosition, endPosition);
        if (distance != 0)
        {
            speed = speed / distance;
        }
    }

    void Update()
    {
        if (active && !isUsed)
        {
            player.GetComponent<PlayerController>().SetIsAlive(false);
            timer += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer);
            if(timer >= 1)
            {
                player.GetComponent<PlayerController>().gameCamera.GetComponent<CameraFollow2D>().setTarget(door);
                timer2 += Time.deltaTime;
                door.gameObject.GetComponent<VanishingDoor>().active = true;
                if(timer2 >= 1)
                {
                    timer3 += Time.deltaTime * 1.25f;
                    player.GetComponent<PlayerController>().gameCamera.GetComponent<CameraFollow2D>().setTarget(player);
                    if (timer3 >= 1)
                    {
                        player.GetComponent<PlayerController>().SetIsAlive(true);
                        isUsed = true;
                    }
                }
            }
        }
    }

    public float GetTimer
    {
        get
        {
            return timer;
        }
    }

    public bool Used
    {
        get
        {
            return isUsed;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 centerPoint = transform.position;
        centerPoint.y -= 2f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(centerPoint, GetComponent<BoxCollider2D>().size * 2);
    }
}

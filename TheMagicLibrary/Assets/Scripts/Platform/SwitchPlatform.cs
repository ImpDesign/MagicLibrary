using UnityEngine;
using System.Collections;

public class SwitchPlatform : MonoBehaviour {

    public GameObject door;
    public bool active = false;
    public bool cameraActive = false;

    private float speed = 2;
    private float timer = 0;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    private Vector3 currentPosition = Vector3.zero;

    void Start()
    {
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
        if (active)
        {
            timer += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer);
            currentPosition = transform.position;

            if(cameraActive)
            {
                active = false;
                door.gameObject.GetComponent<VanishingDoor>().active = true;
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
            return !door.gameObject.GetComponent<BoxCollider2D>().enabled;
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

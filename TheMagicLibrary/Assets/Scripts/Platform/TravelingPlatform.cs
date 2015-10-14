using UnityEngine;
using System.Collections;

public class TravelingPlatform : MonoBehaviour {

    public Vector3 endPosition = Vector3.zero;
    public float speed = 1;
    public bool active = false;

    private Vector3 startPosition = Vector3.zero;
    private Vector3 currentPosition = Vector3.zero;
    private Vector3 destination = Vector3.zero;
    private float timer = 0;
    public bool needsReset = true;

    void Start()
    {
        startPosition = gameObject.transform.position;
        currentPosition = startPosition;
        endPosition = endPosition + startPosition;

        float distance = Vector3.Distance(startPosition, endPosition);
        if (distance != 0)
        {
            speed = speed / distance;
        }
    }

    void Update()
    {
        timer += Time.deltaTime * speed;

        if (active)
        {
            if(!needsReset)
            {
                timer = 0;
                destination = currentPosition;
            }
            this.transform.position = Vector3.Lerp(destination, endPosition, timer);
            currentPosition = transform.position;
            needsReset = true;
        }
        else
        {
            if(needsReset)
            {
                timer = 0;
                destination = currentPosition;
            }
            this.transform.position = Vector3.Lerp(destination, startPosition, timer);
            currentPosition = transform.position;
            needsReset = false;
        }
        active = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, endPosition + this.transform.position);
    }
}

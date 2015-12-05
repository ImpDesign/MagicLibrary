using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

    public float flickerPercentage = .1f;
    public float flickerSpeed = 16f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool outgoing = true;
    private float timer;

	void Start ()
    {
        startPosition = transform.position;
        endPosition = startPosition;
        endPosition.z -= endPosition.z * flickerPercentage;

        float distance = Vector3.Distance(startPosition, endPosition);
        if (distance != 0)
        {
            flickerSpeed = flickerSpeed / distance;
        }
    }
	
	void Update ()
    {
        timer += Time.deltaTime * flickerSpeed;
        startPosition.x = GetComponentInParent<Transform>().position.x;
        startPosition.y = GetComponentInParent<Transform>().position.y;
        endPosition.x = GetComponentInParent<Transform>().position.x;
        endPosition.y = GetComponentInParent<Transform>().position.y;

        if (outgoing)
        {
            this.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
            if (timer > 1)
            {
                timer = 0;
                outgoing = false;
            }
        }
        else
        {
            this.transform.position = Vector3.Lerp(endPosition, startPosition, timer);
            if (timer > 1)
            {
                timer = 0;
                outgoing = true;
            }
        }
    }
}

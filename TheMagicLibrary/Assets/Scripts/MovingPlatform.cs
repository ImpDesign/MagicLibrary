using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Vector3 endPosition = Vector3.zero;
	public float speed = 1;


	private float timer = 0;
	private Vector3 startPosition = Vector3.zero;

	void Start ()
    {
		startPosition = this.gameObject.transform.position;
		endPosition = endPosition + startPosition;
		
		float distance = Vector3.Distance (startPosition, endPosition);
		if (distance != 0)
        {
			speed = speed/distance;
		}
	}
	
	// Update is called once per frame startPosition = Vector3.zero;
	private bool outgoing = true;

	// Use this for initialization
	void Update () {

		timer += Time.deltaTime * speed;

		if (outgoing)
        {
			this.transform.position = Vector3.Lerp (startPosition, endPosition, timer);
			if(timer > 1)
            {
				timer = 0;
				outgoing = false;
			}
		}
        else
        {
			this.transform.position = Vector3.Lerp (endPosition, startPosition, timer);
			if(timer > 1)
            {
				timer = 0;
				outgoing = true;
			}
		}

	
	}

	void OnDrawGizmos ()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawLine (this.transform.position,endPosition + this.transform.position);
	}
}

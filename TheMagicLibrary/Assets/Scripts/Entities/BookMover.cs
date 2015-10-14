using UnityEngine;
using System.Collections;

public class BookMover : MonoBehaviour {

    public int direction;
    public float speed;
    public Vector3 startPosition = Vector3.zero;
    public Vector3 endPosition = Vector3.zero;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime * speed;
        if (transform.position != endPosition)
        {
            this.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

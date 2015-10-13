using UnityEngine;
using System.Collections;

public class TimerSpiderAI : MonoBehaviour {

    public float range;
    public float speed = 10f;
    public float pause = 1f;

    private float timer = 0;
    private float pauseTimer = 0;
    private bool outgoing = true;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;

    void Start()
    {

        startPosition = this.gameObject.transform.position;
        endPosition = this.gameObject.transform.position + new Vector3(0, -range, 0);

        float distance = Vector3.Distance(startPosition, new Vector3(startPosition.x, startPosition.y - range));
        if (distance != 0)
        {
            speed = speed / distance;
        }
    }

    void Update()
    {
        timer += Time.deltaTime * speed;

        if (pauseTimer > 0)
        {
            pauseTimer -= Time.deltaTime;
            timer = 0;
        }
        else if (outgoing)
        {
            this.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
            if (timer > 1)
            {
                timer = 0;
                outgoing = false;
                pauseTimer = pause;
            }
        }
        else
        {
            this.transform.position = Vector3.Lerp(endPosition, startPosition, timer);
            if (timer > 1)
            {
                timer = 0;
                outgoing = true;
                pauseTimer = pause;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(this.transform.position, new Vector3(0, -range - 1, 0) + this.transform.position);
    }
}

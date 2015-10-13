using UnityEngine;
using System.Collections;

public class StrikerSpiderAI : MonoBehaviour {

    public GameObject player;
    public Vector3 range;
    public float speed = 8;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 lastSpot;
    private float timer = 0;
    private bool goingUp = false;

    void Start()
    {

        startPosition = this.gameObject.transform.position;
        endPosition = this.gameObject.transform.position;
        lastSpot = this.gameObject.transform.position;

        float distance = Vector3.Distance(startPosition, new Vector3(startPosition.x, startPosition.y - range.y));
        speed = speed / distance;
    }

    void Update()
    {

        timer += Time.deltaTime;
        endPosition.y = player.transform.position.y + 0.25f;

        if (player.transform.position.x >= (startPosition.x - range.x) &&
            player.transform.position.x <= (startPosition.x + range.x) &&
            player.transform.position.y < startPosition.y &&
            player.transform.position.y > (startPosition.y - range.y))
        {
            if (goingUp)
            {
                timer = 0;
                lastSpot = this.transform.position;
            }
            this.transform.position = Vector3.Lerp(lastSpot, endPosition, timer);
            lastSpot = this.gameObject.transform.position;
            goingUp = false;
        }
        else
        {
            if (!goingUp)
            {
                timer = 0;
                lastSpot = this.transform.position;
            }
            this.transform.position = Vector3.Lerp(lastSpot, startPosition, timer / 4);
            goingUp = true;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "FireBall")
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 lineDraw = range;
        lineDraw.y = -(range.y);
        Gizmos.DrawLine(this.transform.position, lineDraw + this.transform.position);

        lineDraw.x = -range.x;
        Gizmos.DrawLine(this.transform.position, lineDraw + this.transform.position);
    }

}

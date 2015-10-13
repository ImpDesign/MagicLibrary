using UnityEngine;
using System.Collections;

public class QueenSpiderAI : MonoBehaviour {

    public GameObject player;
    public Vector3 range;
    public float lightRange = 1.5f;
    public float speed = 8;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 lastSpot;
    private float timer = 0;
    private bool goingUp = false;
    private PlayerController spells;
    private GameObject light1;
    private GameObject light2;
    private bool burn1;
    private bool burn2;

    void Start()
    {

        startPosition = this.gameObject.transform.position;
        endPosition = this.gameObject.transform.position;
        lastSpot = this.gameObject.transform.position;
        spells = player.GetComponent<PlayerController>();
        burn1 = false;
        burn2 = false;

        float distance = Vector3.Distance(startPosition, new Vector3(startPosition.x, startPosition.y - range.y));
        speed = speed / distance;
    }

    void Update()
    {
        light1 = spells.GetLight1();
        light2 = spells.GetLight2();
        timer += Time.deltaTime;

        if (light1 != null)
        {
            if (light1.transform.position.x >= (startPosition.x - lightRange) &&
                 light1.transform.position.x <= (startPosition.x + lightRange) && 
                 light1.transform.position.y < startPosition.y && 
                 light1.transform.position.y > (startPosition.y - range.y))
            {
                burn1 = true;
            }
            else
            {
                burn1 = false;
            }
        }
        if (light2 != null)
        {
            if (light2.transform.position.x >= (startPosition.x - lightRange) &&
                light2.transform.position.x <= (startPosition.x + lightRange) &&
                light2.transform.position.y < startPosition.y &&
                light2.transform.position.y > (startPosition.y - range.y))
            {
                burn2 = true;
            }
            else
            {
                burn2 = false;
            }
        }
        if (burn1 || burn2)
        {
            if(burn1 && burn2)
            {
                if(light1.transform.position.y > light2.transform.position.y)
                {
                    if(endPosition.y != light1.transform.position.y + 3f)
                    {
                        endPosition.y = light1.transform.position.y + 3f;
                        timer = 0;
                    }
                }
                else
                {
                    if(endPosition.y != light2.transform.position.y + 3f)
                    {
                        endPosition.y = light2.transform.position.y + 3f;
                        timer = 0;
                    }
                }
            }
            else if (burn1)
            {
                if(endPosition.y != light1.transform.position.y + 3f)
                {
                    endPosition.y = light1.transform.position.y + 3f;
                    timer = 0;
                }
            }
            else if (burn2)
            {
                if(endPosition.y != light2.transform.position.y + 3f)
                {
                    endPosition.y = light2.transform.position.y + 3f;
                    timer = 0;
                }
            }
        }
        else
        {
            endPosition.y = player.transform.position.y + 0.25f;
        }

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 lineDraw = range;
        lineDraw.y = -(range.y);
        Gizmos.DrawLine(this.transform.position, lineDraw + this.transform.position);
        Gizmos.DrawLine(this.transform.position + new Vector3(range.x, 0, 0), lineDraw + this.transform.position);

        lineDraw.x = -range.x;
        Gizmos.DrawLine(this.transform.position, lineDraw + this.transform.position);
        Gizmos.DrawLine(this.transform.position - new Vector3(range.x, 0, 0), lineDraw + this.transform.position);

        Gizmos.color = Color.white;

        lineDraw = range;
        lineDraw.y = -(range.y);
        lineDraw.x = lightRange;
        Gizmos.DrawLine(this.transform.position, lineDraw + this.transform.position);
        Gizmos.DrawLine(this.transform.position + new Vector3(lightRange, 0, 0), lineDraw + this.transform.position);

        lineDraw.x = -lightRange;
        Gizmos.DrawLine(this.transform.position, lineDraw + this.transform.position);
        Gizmos.DrawLine(this.transform.position - new Vector3(lightRange, 0, 0), lineDraw + this.transform.position);
    }
}

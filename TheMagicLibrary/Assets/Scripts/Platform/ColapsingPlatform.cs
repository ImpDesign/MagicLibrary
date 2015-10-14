using UnityEngine;
using System.Collections;

public class ColapsingPlatform : MonoBehaviour {

    public Vector3 endPosition = new Vector3(0, -0.2f, 0);
    public float speed = 2;
    public float fuze = 2;
    public float reset = 5;
    public bool set = false;


    private float timer = 0;
    private Vector3 startPosition = Vector3.zero;

    private bool active = false;
    private bool falling = false;
    private bool check = false;
    private float fuzeCopy;
    private float resetCopy;
    private bool outgoing = true;

    void Start()
    {
        startPosition = this.gameObject.transform.position;
        endPosition = endPosition + startPosition;
        fuzeCopy = fuze;
        resetCopy = reset;

        float distance = Vector3.Distance(startPosition, endPosition);
        if (distance != 0)
        {
            speed = speed / distance;
        }
    }


    void Update()
    {
        if(set)
        {
            if(!check)
            {
                active = true;
            }
            check = true;
        }
        if (active)
        {
            timer += Time.deltaTime * speed;

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
            if (fuze <= 0)
            {
                active = false;
                falling = true;
                timer = reset;
            }
            fuze -= Time.deltaTime;

        }
        if(falling)
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * 16, Space.World);
            reset -= Time.deltaTime;
            if(reset <= 0)
            {
                this.transform.position = startPosition;
                set = false;
                check = false;
                active = false;
                falling = false;
                fuze = fuzeCopy;
                reset = resetCopy;
            }
        }
    }
}

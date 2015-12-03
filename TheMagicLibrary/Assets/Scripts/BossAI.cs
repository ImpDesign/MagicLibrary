using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

    public float lightRange = 1.5f;
    public float speed = 8f;
    public float animationLenth;

    private AnimationController2D _animator;
    private GameObject player;
    private GameObject boss;
    private bool moving = false;
    private float timer = 0;
    private float animTimer = 0;
    private int currentPosition = 0;
    private int maxPositions = 0;
    private Vector3[] destinations;
    private Vector3 start;
    private Vector3 finish;


    void Start () {

        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("DarkPlayer");
        }

        boss = GameObject.Find("Boss");

        _animator = gameObject.GetComponent<AnimationController2D>();

        destinations = new Vector3[transform.childCount + 1];
        destinations[0] = transform.position;
        maxPositions = 1;

        for(int i = 0; i > transform.childCount; i++)
        {
            destinations[i + 1] = transform.GetChild(i).position;
            maxPositions++;
        }

	
	}


	void Update ()
    {
        if(moving)
        {
            timer += Time.deltaTime * speed;
            this.transform.position = Vector3.Lerp(start, finish, timer);
            if (timer > 1)
            {
                timer = 0;
                moving = false;
            }
        }

        if(animTimer > animationLenth)
        {
            string anim = "Eye" + Random.Range(1, 5);
            Debug.Log(anim);
            _animator.setAnimation(anim);
            animTimer = 0;
        }

        animTimer += Time.deltaTime;

    }

    public void Move ()
    {
        start = destinations[currentPosition];
        if (currentPosition != maxPositions)
        {
            currentPosition++;
        }
        else
        {
            currentPosition = 0;
        }
        finish = destinations[currentPosition];
        moving = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D");
        if(col.tag == "MovingPlatform")
        {
            Debug.Log("TakeDamage");
            boss.GetComponent<BossHealth>().TakeDamage();
        }
    }


    }

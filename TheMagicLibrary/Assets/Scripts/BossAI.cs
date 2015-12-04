using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

    public float lightRange = 1.5f;
    public float speed = 8f;
    public float strikeSpeed = 16f;
    public float animationLength = 1;
    public float damageDelay = 1f;
    public bool teir2;
    public bool teir3;
    public GameObject[] positions;

    private AnimationController2D _animator;
    private GameObject player;
    private GameObject boss;
    private bool moving = false;
    private bool newAnimation = false;
    private bool attacking = false;
    private bool secondAttack = false;
    private bool startPosSet = false;
    private float speedActual;
    private float strikeSpeedActual;
    private float moveTimer = 0;
    private float attackTimer = 0;
    private float animTimer = 0;
    private float idleTimer = 0;
    private float delayTimer = 0;
    private int currentPosition = 0;
    private int maxPositions = 0;
    private Vector3[] destinations;
    private Vector3 start;
    private Vector3 middle;
    private Vector3 finish;
    private string anim;


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

        destinations = new Vector3[positions.Length + 1];
        destinations[0] = transform.position;
        for(int i = 0; i < positions.Length; i++)
        {
            destinations[i + 1] = positions[i].transform.position;
            maxPositions++;

        }

        if(teir2 || teir3)
        {
            GetComponent<MovingEye>().enabled = true;
        }
        else
        {
            GetComponent<MovingEye>().enabled = false;
        }

        animTimer = Random.Range(0, animationLength);
	}


	void Update ()
    {
        if(moving)
        {
            if(!startPosSet)
            {
                start = transform.position;
                startPosSet = true;
            }
            if(delayTimer > damageDelay)
            {
                if(attacking)
                {
                    this.transform.position = Vector3.Lerp(start, middle, attackTimer);
                    attackTimer += Time.deltaTime * strikeSpeedActual;
                }
                else
                {
                    this.transform.position = Vector3.Lerp(middle, finish, moveTimer);
                    moveTimer += Time.deltaTime * speedActual;
                    if(moveTimer > 1)
                    {
                        moveTimer = 0;
                        attackTimer = 0;
                        delayTimer = 0;
                        moving = false;
                        secondAttack = false;
                        startPosSet = false;
                        if(teir2 || teir3)
                        {
                            GetComponent<MovingEye>().enabled = true;
                            GetComponent<MovingEye>().SetStart();
                        }
                    }
                }
                if (attackTimer > 1)
                {
                    if((teir2||teir3) && (secondAttack == false))
                    {
                        delayTimer = 0;
                        attackTimer = 0;
                        secondAttack = true;
                        startPosSet = false;
                    }
                    else
                    {
                        if (attacking)
                        {
                            delayTimer = 0;
                            attacking = false;
                            _animator.setAnimation("Moving");
                        }
                    }
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
                if(attacking)
                {
                    middle = player.transform.position;
                    float distance = Vector3.Distance(start, middle);
                    if (distance != 0)
                    {
                        strikeSpeedActual = strikeSpeed / distance;
                    }

                    distance = Vector3.Distance(start, middle);
                    if (distance != 0)
                    {
                        speedActual = speed / distance;
                    }
                }
            }
        }
        else
        {
            if (newAnimation)
            {
                _animator.setAnimation(anim);
                newAnimation = false;
            }

            if (animTimer > animationLength)
            {
                anim = "Eye" + Random.Range(1, 5);
                _animator.setAnimation("Idle");
                newAnimation = true;
                animTimer = 0;
            }
        }

        animTimer += Time.deltaTime;

    }

    public void Move ()
    {
        GetComponent<MovingEye>().enabled = false;
        if (currentPosition != maxPositions)
        {
            currentPosition++;
        }
        else
        {
            currentPosition = 0;
        }
        finish = destinations[currentPosition];
        _animator.setAnimation("Angry");
        moving = true;
        attacking = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(Time.fixedTime > 2)
        {
            if (col.tag == "MovingPlatform")
            {
                if (!moving)
                {
                    if((col.transform.position.y < transform.position.y +2) && (col.transform.position.y > transform.position.y -2)
                        && (col.transform.position.x < transform.position.x +2) && (col.transform.position.x > transform.position.x -2))
                    {
                        boss.GetComponent<BossHealth>().TakeDamage();
                        Move();
                    }
                }
            }
        }
    }


    }

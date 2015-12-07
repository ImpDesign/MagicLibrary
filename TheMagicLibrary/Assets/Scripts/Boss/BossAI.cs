using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

    public float lightRange = 1.5f;
    public float speed = 8f;
    public float strikeSpeed = 16f;
    public float animationLength = 1;
    public float damageDelay = 1f;
    public bool tier2;
    public bool tier3;
    public GameObject[] positions;

    private AnimationController2D _animator;
    private GameObject player;
    private GameObject boss;
    private GameObject light1;
    private GameObject light2;
    private GameObject burningLight;
    private bool moving = false;
    private bool newAnimation = false;
    private bool attacking = false;
    private bool secondAttack = false;
    private bool startPosSet = false;
    private bool burn = false;
    private bool burnMoving = false;
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

        if(tier2)
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
        if (moving)
        {
            //MOVING
            if(!startPosSet)
            {
                start = transform.position;
                startPosSet = true;
            }
            if(delayTimer > damageDelay)
            {
                if (attacking)
                {
                    //MOVE TO PLAYER
                    this.transform.position = Vector3.Lerp(start, middle, attackTimer);
                    attackTimer += Time.deltaTime * strikeSpeedActual;
                }
                else
                {
                    //MOVE TO FINAL POSITION
                    transform.position = Vector3.Lerp(middle, finish, moveTimer);
                    moveTimer += Time.deltaTime * speedActual;
                    if(moveTimer > 1)
                    {
                        moveTimer = 0;
                        attackTimer = 0;
                        delayTimer = 0;
                        moving = false;
                        secondAttack = false;
                        startPosSet = false;

                        if (tier2 || (tier3 && (currentPosition % 2 == 1)))
                        {
                            GetComponent<MovingEye>().enabled = true;
                            GetComponent<MovingEye>().SetStart();
                        }
                    }
                }
                if (attackTimer > 1)
                {
                    //RESET FOR NEXT ATTACK
                    if ((tier2||tier3) && (secondAttack == false))
                    {
                        delayTimer = 0;
                        attackTimer = 0;
                        secondAttack = true;
                        startPosSet = false;
                    }
                    //RESET AND BEGIN MOVING
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
                //CALCULATE PLAYER'S POS
                delayTimer += Time.deltaTime;
                if(attacking)
                {
                    Debug.Log("BAD");
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
            //NOT MOVING

            if (tier3)
            {
                light1 = player.GetComponent<PlayerController>().GetLight1();
                light2 = player.GetComponent<PlayerController>().GetLight2();

                //NOT BURNED
                if (!burn)
                {
                    if (light1 != null)
                    {
                        if ((light1.transform.position.x < (transform.position.x + 6) &&
                            light1.transform.position.x > (transform.position.x - 6) &&
                            light1.transform.position.y < (transform.position.y + 6) &&
                            light1.transform.position.y > (transform.position.y - 6)))
                        {
                            burn = true;
                            burningLight = light1;
                            middle = transform.position;
                            Flee();

                            float distance = Vector3.Distance(middle, finish);
                            if (distance != 0)
                            {
                                speedActual = speed / distance;
                            }
                        }
                    }


                    if (light2 != null)
                    {
                        if ((light2.transform.position.x < (transform.position.x + 6) &&
                            light2.transform.position.x > (transform.position.x - 6) &&
                            light2.transform.position.y < (transform.position.y + 6) &&
                            light2.transform.position.y > (transform.position.y - 6)))
                        {
                            burn = true;
                            burningLight = light2;
                            middle = transform.position;
                            Flee();

                            float distance = Vector3.Distance(middle, finish);
                            if (distance != 0)
                            {
                                speedActual = speed / distance;
                            }
                        }
                    }
                }
                else
                {
                    //IS BURNED
                    if (burningLight != light1 && burningLight != light2)
                    {
                        //IF LIGHT GOES AWAY
                        burn = false;
                        middle = transform.position;
                        GoBack();

                        float distance = Vector3.Distance(middle, finish);
                        if (distance != 0)
                        {
                            speedActual = speed / distance;
                        }
                    }
                }
            }

            //IDLE ANIMATIONS
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
        Flee();
        _animator.setAnimation("Angry");
        attacking = true;
    }

    public void Flee ()
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
        _animator.setAnimation("Moving");
        moving = true;
    }

    public void GoBack()
    {
        GetComponent<MovingEye>().enabled = false;
        if (currentPosition != 0)
        {
            currentPosition--;
        }
        else
        {
            currentPosition = maxPositions;
        }
        finish = destinations[currentPosition];
        _animator.setAnimation("Moving");
        moving = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "MovingPlatform")
        {
            if (!moving)
            {
                if ((col.transform.position.y < transform.position.y + 2) && (col.transform.position.y > transform.position.y - 2)
                    && (col.transform.position.x < transform.position.x + 2) && (col.transform.position.x > transform.position.x - 2))
                {
                    boss.GetComponent<BossHealth>().TakeDamage();
                    boss.GetComponent<TentacleActivate>().AttackPlayer(tier2, tier3);
                    Move();
                }
            }
        }
        else if (col.tag == "FireBall")
        {
            if(moving)
            {
                if ((col.transform.position.y < transform.position.y + 2) && (col.transform.position.y > transform.position.y - 2)
                    && (col.transform.position.x < transform.position.x + 2) && (col.transform.position.x > transform.position.x - 2))
                {
                    boss.GetComponent<BossHealth>().TakeDamage();
                    DestroyObject(col.gameObject);
                }
            }
        }
    }


    }

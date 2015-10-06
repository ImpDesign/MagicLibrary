﻿using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;
using System.Threading;

public class PlayerController : MonoBehaviour {

	public GameObject gameCamera;
    public GameObject invisiblePlatformList;
    public GameObject trickPlatformList;
    public GameObject deathBlur;
	//public GameObject healthbar;
	//public GameObject gameOverPanel;

	Vector3 velocity = Vector3.zero;
	public float speed = 5;
	public float gravity = -1;
	public float spring = 100;

	public int health = 100;

	private CharacterController2D _controller;
	private AnimationController2D _animator;

	private int currentHealth = 0;
    private float fireTime = 0;
    private int direction = 1;
    private bool isAlive = true;
    private bool canJump = false;
    private bool canDoubleJump = false;
    private bool canReveal = false;
    private bool cameraDerp = true;

    public bool doubleJump = false;
    public bool fireBolt = false;
    public bool revealSpell = false;
    public bool lightSpell = false;

    private GameObject light1;
    private GameObject light2;
    private GameObject newLight;
    private int oldLight = 2;

    // Use this for initialization
    void Start () {

		_controller = gameObject.GetComponent<CharacterController2D>();
		_animator = gameObject.GetComponent<AnimationController2D>();
		currentHealth = health;
        cameraDerp = true;
        StartCoroutine("FadeInSequence");
	}
	
	// Update is called once per frame
	void Update () {

        //Used to start Camera2D Follow script once the level is restart
        if(cameraDerp)
        {
            gameCamera.gameObject.GetComponent<CameraFollow2D>().startCameraFollow();
            cameraDerp = false;
        }

        //Get the last velocity the player had
		velocity =  _controller.velocity;

        //Moving platform exception
		if (_controller.isGrounded && (_controller.ground != null) && (_controller.ground.tag == "MovingPlatform"))
        {
            
			this.transform.parent = _controller.ground.transform;
            //Colapsing platform
            if (_controller.ground.GetComponent<ColapsingPlatform>() != null)
            {
                _controller.ground.GetComponent<ColapsingPlatform>().set = true;
            }
            
        } 
		else
        {

			if( this.transform.parent != null)
            {
				this.transform.parent = null;
			}
		}
        /*/Colapsing platform exception
        if (_controller.isGrounded && (_controller.ground != null) && (_controller.ground.tag == "ColapsingPlatform"))
        {
            this.transform.parent = _controller.ground.transform;
        }
        else
        {

            if (this.transform.parent != null)
            {
                this.transform.parent = null;
            }
        }*/

        velocity.x = 0;

        //If the player is still alive, do everything here
		if (isAlive) {

            //If you need to reset some variables when the player is grounded, do it here
            if(_controller.isGrounded)
            {
                canJump = true;
                if (doubleJump)
                {
                    canDoubleJump = true;
                }
            }

            //Running left
			if (Input.GetAxis ("Horizontal") < 0) {

				velocity.x = -speed;
				if (_controller.isGrounded)
                {
					//_animator.setAnimation ("Run");
				}
                //_animator.setFacing ("Left");
                canReveal = false;
                direction = -1;
			}
            //Running right
            else if (Input.GetAxis ("Horizontal") > 0)
            {
				velocity.x = speed;
				if (_controller.isGrounded)
                {
					//_animator.setAnimation ("Run");
				}
                //_animator.setFacing ("Right");
                canReveal = false;
                direction = 1;
			}
            else
            {
				if (_controller.isGrounded)
                {
                    //_animator.setAnimation ("Idle");
                    if (revealSpell) { canReveal = true; }
				}
			}
            //When jumping
			if (Input.GetKeyDown(KeyCode.Space) && (canJump||canDoubleJump))
            {
				velocity.y = Mathf.Sqrt (2f * spring * -gravity);
				//_animator.setAnimation ("Jump");

                if(!_controller.isGrounded)
                {
                    canDoubleJump = false;
                }
                canJump = false;
                canReveal = false;
			}
            //Reveal Spell
            if (Input.GetKey(KeyCode.LeftControl) && canReveal)
            {
                foreach (SpriteRenderer s in invisiblePlatformList.GetComponentsInChildren<SpriteRenderer>())
                {
                    s.enabled = true;
                }
                foreach (SpriteRenderer s in trickPlatformList.GetComponentsInChildren<SpriteRenderer>())
                {
                    s.enabled = false;
                }
            }
            else
            {
                foreach (SpriteRenderer s in invisiblePlatformList.GetComponentsInChildren<SpriteRenderer>())
                {
                    if(s.gameObject != this.gameObject)
                    {
                        s.enabled = false;
                    }
                }
                foreach (SpriteRenderer s in trickPlatformList.GetComponentsInChildren<SpriteRenderer>())
                {
                    s.enabled = true;
                }
            }
            //For Light Spell
            if (Input.GetKey(KeyCode.LeftAlt) && lightSpell)
            {
                fireTime += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt) && lightSpell)
            {
                LightScript light = GetComponent<LightScript>();
                if (light != null)
                {
                    newLight = light.Attack(fireTime, direction);
                    if (oldLight == 1)
                    {
                        Destroy(light1);
                        light1 = newLight;
                        oldLight = 2;
                    }
                    else
                    {
                        Destroy(light2);
                        light2 = newLight;
                        oldLight = 1;
                    }
                }
                fireTime = 0;
            }
        }

		velocity.y += gravity;
		_controller.move (velocity * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "KillZ")
        {

            PlayerFallDeath();

        }
        else if (col.tag == "Damaging")
        {

            PlayerDamage(25);

        }

    }

    private void PlayerDeath()
    {

		isAlive = false;
		//_animator.setAnimation("Death");
		//gameOverPanel.SetActive (true);

	}

	private void PlayerDamage(int damage)
    {

		currentHealth -= damage;

		float normalizedHealth = (float)currentHealth / (float)health;

		//healthbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (normalizedHealth * 256, 32);
		if (currentHealth <= 0)
        {
			PlayerDeath ();
		}
	}

	private void PlayerFallDeath()
    {
		currentHealth = 0;
		//healthbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0 , 32);
		gameCamera.gameObject.GetComponent<CameraFollow2D>().stopCameraFollow();
        DeathBlur();

	}

    private void DeathBlur()
    {
        StopAllCoroutines();
        StartCoroutine("FadeOutSequence");
        StartCoroutine("WaitAndRestart");
    }

    IEnumerator FadeOutSequence ()
    {
        Color color = Color.black;
        color.a = 0;
        while(color.a < 255)
        {
            deathBlur.GetComponent<Image>().color = color;
            color.a += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeInSequence()
    {
        Color color = Color.black;
        while (color.a > 0)
        {
            deathBlur.GetComponent<Image>().color = color;
            color.a -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(1);
        Application.LoadLevel(Application.loadedLevel);
    }
}

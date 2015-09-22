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
	private bool isAlive = true;
    private bool canJump = false;
    private bool canDoubleJump = false;
    private bool canReveal = false;
    private bool cameraDerp = true;

    public bool doubleJump = false;
    public bool fireBolt = false;
    public bool revealSpell = false;
    public bool lightSpell = false;

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

        if(cameraDerp)
        {
            gameCamera.gameObject.GetComponent<CameraFollow2D>().startCameraFollow();
            cameraDerp = false;
        }

		velocity =  _controller.velocity;

		if (_controller.isGrounded && (_controller.ground != null) && (_controller.ground.tag == "MovingPlatform"))
        {
			this.transform.parent = _controller.ground.transform;
		} 
		else
        {

			if( this.transform.parent != null)
            {
				this.transform.parent = null;
			}
		}

		velocity.x = 0;

		if (isAlive) {

            if(_controller.isGrounded)
            {
                canJump = true;
                if (doubleJump)
                {
                    canDoubleJump = true;
                }
            }

			if (Input.GetAxis ("Horizontal") < 0) {

				velocity.x = -speed;
				if (_controller.isGrounded)
                {
					//_animator.setAnimation ("Run");
				}
                //_animator.setFacing ("Left");
                canReveal = false;
			}
            else if (Input.GetAxis ("Horizontal") > 0)
            {
				velocity.x = speed;
				if (_controller.isGrounded)
                {
					//_animator.setAnimation ("Run");
				}
                //_animator.setFacing ("Right");
                canReveal = false;
			}
            else
            {
				if (_controller.isGrounded)
                {
                    //_animator.setAnimation ("Idle");
                    if (revealSpell) { canReveal = true; }
				}
			}

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
                    s.enabled = false;
                }
                foreach (SpriteRenderer s in trickPlatformList.GetComponentsInChildren<SpriteRenderer>())
                {
                    s.enabled = true;
                }
            }
        }

		velocity.y += gravity;
		_controller.move (velocity * Time.deltaTime);

    }

	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.tag == "KillZ")
        {
			PlayerFallDeath ();
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

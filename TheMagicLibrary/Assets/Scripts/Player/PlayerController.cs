using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;
using System.Threading;

public class PlayerController : MonoBehaviour {

	public GameObject gameCamera;
    public GameObject invisiblePlatformList;
    public GameObject trickPlatformList;
    public GameObject deathBlur;
    public GameObject pausePanel;
	public GameObject healthbar;
	Vector3 velocity = Vector3.zero;
	public float speed = 5;
	public float gravity = -1;
	public float spring = 100;
    public float poisonRate = 2f;
    public float nightvisionRate = 20f;
	public int health = 100;
    public int poisonDamage = 1;
    public int spiderDamage = 25;
    public int platformDamage = 10;
    public bool doubleJump = false;
    public bool fireBolt = false;
    public bool revealSpell = false;
    public bool lightSpell = false;
    public bool canTeleport = true;
    public bool isTeleporting = false;

    private CharacterController2D _controller;
	private AnimationController2D _animator;
	private int currentHealth = 0;
    private float fireTime = 0;
    private float pushBackTime = 0;
    private float poisonTimer = 0;
    private float nightvisionTimer = 0;
    //private float walkingTimer = 0;
    private int direction = 1;
    private bool isAlive = true;
    private bool isPoisoned = false;
    private bool canJump = false;
    private bool canDoubleJump = false;
    private bool canReveal = false;
    private bool cameraDerp = true;
    private bool startNextLevel = false;
    private GameObject light1;
    private GameObject light2;
    private GameObject newLight;
    private int oldLight = 2;
    private Collider2D endingCollider = null;
    private Vector3 endingPosition = Vector3.zero;

    void Start () {

		_controller = gameObject.GetComponent<CharacterController2D>();
		_animator = gameObject.GetComponent<AnimationController2D>();
		currentHealth = health;
        cameraDerp = true;
        StartCoroutine("FadeInSequence");
	}
	
	void Update () {

        //Used to start Camera2D Follow script once the level is restart
        if(cameraDerp)
        {
            gameCamera.gameObject.GetComponent<CameraFollow2D>().startCameraFollow();
            cameraDerp = false;
        }

        //Get the last velocity the player had
		velocity =  _controller.velocity;

        //Pausing the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

        //Moving platform exception
            if (_controller.isGrounded && (_controller.ground != null) && (_controller.ground.tag == "MovingPlatform"))
        {
            
			this.transform.parent = _controller.ground.transform;
            //Colapsing platform
            if (_controller.ground.GetComponent<ColapsingPlatform>() != null)
            {
                _controller.ground.GetComponent<ColapsingPlatform>().set = true;
            }
            //Traveling platform
            if (_controller.ground.GetComponent<TravelingPlatform>() != null)
            {
                _controller.ground.GetComponent<TravelingPlatform>().active = true;
                if(!canTeleport)
                    _animator.setAnimation("Idle");
            }
            //Switch platform
            if (_controller.ground.GetComponent<SwitchPlatform>() != null)
            {
                _controller.ground.GetComponent<SwitchPlatform>().active = true;
                if (!_controller.ground.GetComponent<SwitchPlatform>().Used)
                    _animator.setAnimation("Idle");
            }
            //Advanced Switch platform
            if (_controller.ground.GetComponent<AdvancedSwitchPlatform>() != null)
            {
                _controller.ground.GetComponent<AdvancedSwitchPlatform>().active = true;
                if(!_controller.ground.GetComponent<AdvancedSwitchPlatform>().Used)
                    _animator.setAnimation("Idle");
            }
        } 
		else
        {

			if( this.transform.parent != null)
            {
				this.transform.parent = null;
			}
		}
        if (_controller.isGrounded && (_controller.ground != null) && (_controller.ground.tag == "Teleport"))
        {
            if (_controller.ground.GetComponent<TeleportingBookshelf>() != null)
            {
                _controller.ground.GetComponent<TeleportingBookshelf>().inPosition = true;
                _controller.ground.GetComponent<TeleportingBookshelf>().canTeleport = canTeleport;
                if(isTeleporting)
                {
                    isAlive = false;
                }
                else
                {
                    isAlive = true;
                }
            }
        }
        else
        {
            canTeleport = true;
        }

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

            //Walking left
			if (Input.GetAxis ("Horizontal") < 0) {

				velocity.x = -speed;
				if (_controller.isGrounded)
                {
					_animator.setAnimation ("Walking");
				}
                _animator.setFacing ("Left");
                canReveal = false;
                direction = -1;
			}
            //Walking right
            else if (Input.GetAxis ("Horizontal") > 0)
            {
				velocity.x = speed;
				if (_controller.isGrounded)
                {
					_animator.setAnimation ("Walking");
				}
                _animator.setFacing ("Right");
                canReveal = false;
                direction = 1;
			}
            else
            {
				if (_controller.isGrounded)
                {
                    if(_animator.getAnimation() != "Idle_Wand")
                        _animator.setAnimation ("Idle");
                    if (revealSpell) { canReveal = true; }
				}
			}
            //When jumping
			if (Input.GetKeyDown(KeyCode.Space) && (canJump||canDoubleJump))
            {
				velocity.y = Mathf.Sqrt (2f * spring * -gravity);
				_animator.setAnimation ("Jump");

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
                foreach (LineRenderer l in invisiblePlatformList.GetComponentsInChildren<LineRenderer>())
                {
                    l.enabled = true;
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
                foreach (LineRenderer l in invisiblePlatformList.GetComponentsInChildren<LineRenderer>())
                {
                    l.enabled = false;
                }
                foreach (SpriteRenderer s in trickPlatformList.GetComponentsInChildren<SpriteRenderer>())
                {
                    s.enabled = true;
                }
            }
            //For Light Spell
            if (Input.GetKey(KeyCode.Z) && lightSpell)
            {
                fireTime += Time.deltaTime;
                _animator.setAnimation("Idle_Wand");
            }
            if (Input.GetKeyUp(KeyCode.Z) && lightSpell)
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
            //For FireBolt Spell
            if (Input.GetKeyDown(KeyCode.X) && fireBolt)
            {
                WeaponScript bolt = GetComponent<WeaponScript>();
                bolt.Attack(direction);
                _animator.setAnimation("Idle_Wand");
            }
            //Pushback for harmful entities
            if (pushBackTime > 0)
            {
                velocity.x += 8 * (-direction);
                pushBackTime -= Time.deltaTime;
            }
            //Posion script
            if (isPoisoned)
            {
                if (poisonTimer <= 0)
                {
                    PlayerDamage(poisonDamage);
                    poisonTimer = poisonRate;
                }
                poisonTimer -= Time.deltaTime;
            }
            //Nightvision script
            if (nightvisionTimer > 0)
            {
                GetComponentInChildren<Light>().enabled = true;
                nightvisionTimer -= Time.deltaTime;
            }
            else
            {
                GetComponentInChildren<Light>().enabled = false;
            }
        }
        //Next Level Load Movement
        if(endingCollider != null)
        {
            endingPosition = endingCollider.transform.position;
            endingPosition.y = gameObject.transform.position.y;
        }
        if (startNextLevel && gameObject.transform.position.x < endingPosition.x)
        {
            velocity.x += 3;
            if (_controller.isGrounded && (_controller.ground != null) && (_controller.ground.tag == "MovingPlatform"))
            {
                this.transform.parent = _controller.ground.transform;
                //Traveling platform
                if (_controller.ground.GetComponent<TravelingPlatform>() != null)
                {
                    _controller.ground.GetComponent<TravelingPlatform>().active = true;
                }
            }
        }
        velocity.y += gravity;
        _controller.move(velocity * Time.deltaTime);
    }

    //Used for Queen Spiders
    public GameObject GetLight1()
    {
        return light1;
    }
    //Used for Queen Spiders
    public GameObject GetLight2()
    {
        return light2;
    }

    public void SetIsAlive(bool input)
    {
        isAlive = input;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "KillZ")
        {

            PlayerFallDeath();

        }
        else if (col.tag == "Damaging")
        {

            PlayerDamage(platformDamage);

        }
        else if (col.tag == "Spider")
        {
            pushBackTime = .25f;
            PlayerDamage(spiderDamage);
        }
        else if (col.tag == "Poison")
        {
            isPoisoned = true;
        }
        else if (col.tag == "Antidote")
        {
            isPoisoned = false;
        }
        else if (col.tag == "PoisonSpider")
        {
            pushBackTime = .25f;
            PlayerDamage(spiderDamage);
            isPoisoned = true;
        }
        else if (col.tag == "Health")
        {
            currentHealth = health;
            healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2(256, 32);
        }
        else if (col.tag == "Nightvision")
        {
            nightvisionTimer = nightvisionRate;
        }
        else if (col.tag == "NextLevel")
        {
            gameCamera.GetComponent<CameraFollow2D>().stopCameraFollow();
            isAlive = false;
            endingCollider = col;
            startNextLevel = true;
            Debug.Log("StartNextLevel");
            StopAllCoroutines();
            StartCoroutine(LoadNextLevel());
        }

    }

    private void PlayerDeath()
    {

		isAlive = false;
        _animator.setAnimation("Death");
        currentHealth = 0;
        healthbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0 , 32);
        DeathBlur();

    }

	private void PlayerDamage(int damage)
    {
        if(isAlive)
        {
            currentHealth -= damage;
            Debug.Log("Player Health = " + currentHealth);

            float normalizedHealth = (float)currentHealth / (float)health;

            healthbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (normalizedHealth * 256, 32);
            if (currentHealth <= 0)
            {
                PlayerDeath();
            }
        }
	}

	private void PlayerFallDeath()
    {
		currentHealth = 0;
		healthbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0 , 32);
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

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        yield return null;
        StartCoroutine(FadeOutSequence());
        yield return new WaitForSeconds(1);
        Application.LoadLevel(Application.loadedLevel+1);
    }
}

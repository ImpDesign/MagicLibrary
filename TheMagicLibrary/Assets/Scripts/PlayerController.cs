using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject gameCamera;
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

	// Use this for initialization
	void Start () {

		_controller = gameObject.GetComponent<CharacterController2D>();
		_animator = gameObject.GetComponent<AnimationController2D>();
		gameCamera.gameObject.GetComponent<CameraFollow2D>().startCameraFollow();
		currentHealth = health;

	}
	
	// Update is called once per frame
	void Update () {

		velocity =  _controller.velocity;

		if (_controller.isGrounded && (_controller.ground != null) && (_controller.ground.tag == "MovingPlatform")) {

			this.transform.parent = _controller.ground.transform;

		} 
		else {

			if( this.transform.parent != null){

				this.transform.parent = null;

			}
		}

		velocity.x = 0;

		if (isAlive) {

			if (Input.GetAxis ("Horizontal") < 0) {

				velocity.x = -speed;
				if (_controller.isGrounded) {
					//_animator.setAnimation ("Run");
				}
				//_animator.setFacing ("Left");

			} else if (Input.GetAxis ("Horizontal") > 0) {

				velocity.x = speed;
				if (_controller.isGrounded) {
					//_animator.setAnimation ("Run");
				}
				//_animator.setFacing ("Right");

			} else {

				if (_controller.isGrounded) {
					//_animator.setAnimation ("Idle");
				}

			}

			if (Input.GetAxis ("Jump") > 0 && _controller.isGrounded) {

				velocity.y = Mathf.Sqrt (2f * spring * -gravity);
				//_animator.setAnimation ("Jump");

			}
		}

		velocity.y += gravity;
		_controller.move (velocity * Time.deltaTime);

    }

	void OnTriggerEnter2D(Collider2D col) {

		if (col.tag == "KillZ") {

			PlayerFallDeath ();

		} 
		else if (col.tag == "Damaging") {

			PlayerDamage(25);

		}

	}

	private void PlayerDeath(){

		isAlive = false;
		//_animator.setAnimation("Death");
		//gameOverPanel.SetActive (true);

	}

	private void PlayerDamage(int damage) {

		currentHealth -= damage;

		float normalizedHealth = (float)currentHealth / (float)health;

		//healthbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (normalizedHealth * 256, 32);
		if (currentHealth <= 0) {
			PlayerDeath ();
		}
	}

	private void PlayerFallDeath() {

		currentHealth = 0;
		//healthbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0 , 32);
		gameCamera.gameObject.GetComponent<CameraFollow2D>().stopCameraFollow();
		//gameOverPanel.SetActive (true);

	}
}

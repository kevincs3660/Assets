using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerController2 : MonoBehaviour {

	public float speed = 8;
	public float gravity = -35;
	public float jumpHeight = 5;

	private CharacterController2D _controller;
	private WeaponScript[] _weapons;
	private bool facingRight = true;
	private int meleeTimer = 50;
	private int dashTime = 20;
	private bool dashEnabled = true;
	private float dashCounter;
	private int dashCooldown = 50;
	private int dashCooldownCounter;
	private bool dashing;
	enum characterStates
	{
		IDLE = 0,
		FACING_RIGHT = 1,
		FACING_LEFT = 2,
		RUNNING = 3,
		JUMPING = 4,
		DASHING = 5,
		MELEE = 6

	};
	private characterStates state = characterStates.IDLE;
	// Use this for initialization

	void Awake()
	{
		dashCounter = dashTime;
		dashCooldownCounter = dashCooldown;
		state = characterStates.IDLE;
		_weapons = GetComponentsInChildren<WeaponScript>();
	}

	void Start () {
		_controller = gameObject.GetComponent<CharacterController2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 velocity = _controller.velocity;
		float inputX = Input.GetAxis ("Horizontal");
		//Vector2 shotDirection = Vector2.zero;
		bool shoot = Input.GetButtonDown ("Fire1");

		//Debug.Log ("Axis is: " + inputX.ToString ());
		//Debug.Log ("State is : " + state);
		switch(state)
		{
		case characterStates.IDLE:
			//Debug.Log("IDLE");
			if(dashEnabled == false)
				dashCooldownCounter--;
			else
				if(dashCounter < dashTime)
					dashCounter+= 0.5f;

			velocity.x = 0;

			if (inputX < 0) {
				velocity.x = -speed;
				facingRight = false;
				state = characterStates.RUNNING;
				//shotDirection = -transform.right;
			} else if (inputX > 0) {
				velocity.x = speed;
				facingRight = true;
				state = characterStates.RUNNING;
				//shotDirection = transform.right;
			}


			if (Input.GetAxis ("Jump") > 0 && _controller.isGrounded) 
			{	
				state = characterStates.JUMPING;
				velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
			}

			if (Input.GetKey (KeyCode.T) == true) {
				if (_weapons[2] != null && _weapons[3] != null)
					if(facingRight)
						_weapons[2].Attack(false);
				else
					_weapons[3].Attack(false);
				state = characterStates.MELEE;
			}


			if (shoot) {

				if (_weapons [0] != null && _weapons [1] != null)
				if (inputX > 0)
					_weapons [0].Attack (false);
				else if (inputX < 0)
					_weapons [1].Attack (false);
				else {
					if (facingRight)
						_weapons [0].Attack (false);
					else
						_weapons [1].Attack (false);
				}
			
				//_weapons[0].Attack(false, shotDirection);
			}

			//Debug.Log("Moving player");
			velocity.y += gravity * Time.deltaTime;

			_controller.move (velocity * Time.deltaTime);
			break;
		case characterStates.RUNNING:

			if(dashEnabled == false)
				dashCooldownCounter--;
			else
				if(dashCounter < dashTime)
					dashCounter+= 0.5f;

			//Debug.Log("Dashcounter: " + dashCounter);

			if(dashCooldownCounter <= 0)
			{
				dashEnabled = true;
				dashCooldownCounter = dashCooldown;
				dashCounter = dashTime;
			}

				velocity.x = 0;
			
			if (inputX < 0) {
				velocity.x = -speed;
				facingRight = false;
				//shotDirection = -transform.right;
			} else if (inputX > 0) {
				velocity.x = speed;
				facingRight = true;
				//shotDirection = transform.right;
			}
			else
				state = characterStates.IDLE;
			
			if (Input.GetKey (KeyCode.LeftShift) == true && dashEnabled) {
				if (inputX < 0)
				{
					velocity.x = -speed * 2;
					dashCounter--;
					if(dashCounter <= 0)
						dashEnabled = false;
				}
				else if(inputX > 0)
				{
					velocity.x = speed * 2;
					dashCounter--;
					if(dashCounter <= 0)
						dashEnabled = false;
				}
				//Debug.Log("DashCounter: " + dashCounter);
			}
			
			if (Input.GetAxis ("Jump") > 0 && _controller.isGrounded) {
				state = characterStates.JUMPING;
				velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
			}

			if (Input.GetKey (KeyCode.T) == true) {
				if (_weapons[2] != null && _weapons[3] != null)
					if(facingRight)
					_weapons[2].Attack(false);
					else
					_weapons[3].Attack(false);
				state = characterStates.MELEE;
				break;
			}
			
			if (shoot) {
				if (_weapons [0] != null && _weapons [1] != null)
					if (inputX > 0)
						_weapons [0].Attack (false);
				else if (inputX < 0)
					_weapons [1].Attack (false);
				else {
					if (facingRight)
						_weapons [0].Attack (false);
					else
						_weapons [1].Attack (false);
				}
				
				//_weapons[0].Attack(false, shotDirection);
			}

			if(state != characterStates.MELEE)
			{
			velocity.y += gravity * Time.deltaTime;
			
			_controller.move (velocity * Time.deltaTime);
			}
			break;
		case characterStates.JUMPING:

			if(dashEnabled == false)
				dashCooldownCounter--;
			else
				if(dashCounter < dashTime)
					dashCounter+= 0.5f;

			// movement
			if (inputX < 0) {
				velocity.x = -speed * 1f;
				facingRight = false;
				//shotDirection = -transform.right;
			} else if (inputX > 0) {
				velocity.x = speed * 1f;
				facingRight = true;
				//shotDirection = transform.right;
			}
			else
			{
				//state = characterStates.IDLE;
				//break;
			}

			if (Input.GetKey (KeyCode.LeftShift) == true && dashEnabled) {
				if (inputX < 0)
				{
					velocity.x = -speed * 2;
					dashCounter--;
					if(dashCounter <= 0)
						dashEnabled = false;
				}
				else if(inputX > 0)
				{
					velocity.x = speed * 2;
					dashCounter--;
					if(dashCounter <= 0)
						dashEnabled = false;
				}
			}

			// shooting
			if (shoot) {
				if (_weapons [0] != null && _weapons [1] != null)
					if (inputX > 0)
						_weapons [0].Attack (false);
				else if (inputX < 0)
					_weapons [1].Attack (false);
				else {
					if (facingRight)
						_weapons [0].Attack (false);
					else
						_weapons [1].Attack (false);
				}
				
				//_weapons[0].Attack(false, shotDirection);
			}

			velocity.y += gravity * Time.deltaTime;
			_controller.move (velocity * Time.deltaTime);

			if(_controller.isGrounded)
				state = characterStates.IDLE; 

			break;
		case characterStates.MELEE:

			if(dashEnabled == false)
				dashCooldownCounter--;
			else
				if(dashCounter < dashTime)
					dashCounter+= 0.5f;

			meleeTimer--;

			if(meleeTimer <= 0)
			{
				state = characterStates.IDLE;
				meleeTimer = 50;
			}

			break;
		}
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			transform.position.y,
			transform.position.z
			);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;
		//Debug.Log ("Player colliding!!!");
		// Collision with enemy
		MeleeEnemyScript enemy = collision.gameObject.GetComponent<MeleeEnemyScript>();
		if (enemy != null)
		{
			// Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
			
			damagePlayer = true;
		}
		
		// Damage the player
		if (damagePlayer)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage(1);
		}
	}

	void OnDestroy()
	{
		// Game Over.
		// Add the script to the parent because the current game
		// object is likely going to be destroyed immediately.
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}
}

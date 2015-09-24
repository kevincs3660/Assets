using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerController : MonoBehaviour {

	public float speed = 8;
	public float gravity = -35;
	public float jumpHeight = 5;

	private CharacterController2D _controller;
	private WeaponScript[] _weapons;
	private bool facingRight = true;
	enum states
	{
		IDLE = 0,
		FACING_RIGHT = 1,
		FACING_LEFT = 2,
		RUNNING = 3,
		JUMPING = 4,
		DASHING = 5

	};
	// Use this for initialization

	void Awake()
	{
		_weapons = GetComponentsInChildren<WeaponScript>();
	}

	void Start () {
		_controller = gameObject.GetComponent<CharacterController2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 velocity = _controller.velocity;
		float inputX = Input.GetAxis ("Horizontal");
		Vector2 shotDirection = Vector2.zero;

		//Debug.Log ("Axis is: " + inputX.ToString ());

		velocity.x = 0;

		if ( inputX < 0) 
		{
			velocity.x = -speed;
			facingRight = false;
			//shotDirection = -transform.right;
		}
		else if (inputX > 0)
		{
			velocity.x = speed;
			facingRight = true;
			//shotDirection = transform.right;
		}

		if (Input.GetKey(KeyCode.LeftShift) == true) {
			if(inputX < 0)
				velocity.x = -speed * 2;
			else 
				velocity.x = speed * 2;
		}

		if (Input.GetAxis ("Jump") > 0 && _controller.isGrounded) {
			velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
		}

		bool shoot = Input.GetButtonDown ("Fire1");
		shoot |= Input.GetButtonDown ("Fire2");

		if (shoot) 
		{
			if(_weapons[0] != null && _weapons[1] != null)
				if(inputX > 0)
					_weapons[0].Attack(false);
				else if(inputX < 0)
					_weapons[1].Attack(false);
				else
				{
					if(facingRight)
					_weapons[0].Attack(false);
					else
					_weapons[1].Attack(false);
				}
			
				//_weapons[0].Attack(false, shotDirection);
		}

		velocity.y += gravity * Time.deltaTime;

		_controller.move (velocity * Time.deltaTime);
	}
}

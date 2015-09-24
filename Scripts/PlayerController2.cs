﻿using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerController2 : MonoBehaviour {

	public float speed = 8;
	public float gravity = -35;
	public float jumpHeight = 5;

	private CharacterController2D _controller;
	private WeaponScript[] _weapons;
	private bool facingRight = true;
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
		Vector2 shotDirection = Vector2.zero;
		bool shoot = Input.GetButtonDown ("Fire1");

		//Debug.Log ("Axis is: " + inputX.ToString ());
		Debug.Log ("State is : " + state);
		switch(state)
		{
		case characterStates.IDLE:
			Debug.Log("IDLE");
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
				if (_weapons[2] != null)
					_weapons[2].Attack(false);
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

			Debug.Log("Moving player");
			velocity.y += gravity * Time.deltaTime;

			_controller.move (velocity * Time.deltaTime);
			break;
		case characterStates.RUNNING:
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
			
			if (Input.GetKey (KeyCode.LeftShift) == true) {
				if (inputX < 0)
					velocity.x = -speed * 2;
				else 
					velocity.x = speed * 2;
			}
			
			if (Input.GetAxis ("Jump") > 0 && _controller.isGrounded) {
				state = characterStates.JUMPING;
				velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
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
			
			velocity.y += gravity * Time.deltaTime;
			
			_controller.move (velocity * Time.deltaTime);
			break;
		case characterStates.JUMPING:


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

			if (Input.GetKey (KeyCode.LeftShift) == true) {
				if (inputX < 0)
					velocity.x = -speed * 2f;
				else 
					velocity.x = speed * 2f;
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
		}
	}
}
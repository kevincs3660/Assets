using UnityEngine;
using System.Collections;

public class MoveTowardPlayerRanged : MonoBehaviour {

	private float playerx;
	private float playery;
	private PlayerController2 player1;

	public Vector2 speed = new Vector2(1, 1);
	private Vector2 direction;

	private Vector2 movement;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("Player");
		player1 = (PlayerController2)player.GetComponent (typeof(PlayerController2));
		//GameObject parent = player.transform.parent.gameObject.transform.position.x;
		//player1 = (PlayerController2)GameObject.Find ("Player");
		//player x = transform.Find("Player").GetComponent.
		//playerx =  player1.transform.position.x; //+ Mathf.Abs(player.transform.parent.gameObject.transform.position.x);
		//playery = player1.transform.position.y;
		direction = (player1.transform.position - this.transform.position).normalized;
		//Debug.Log (this.transform.position.z);
		//Debug.Log ("Player X: " + playerx + " Player Y: " + playery);
		//Debug.Log ("Transform parent x: " + player.transform.parent.gameObject.transform.position.x + " Tranform parent y: " + player.transform.parent.gameObject.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {


		movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
		movement *= Time.deltaTime;
		
		transform.Translate (movement);

	}
}

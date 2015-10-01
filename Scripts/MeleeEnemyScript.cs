using UnityEngine;
using System.Collections;

public class MeleeEnemyScript : MonoBehaviour {
	
	private float playerx;
	private float playery;
	private PlayerController2 player1;
	private bool hasSpawn;
	
	public Vector2 speed = new Vector2(1, 0);
	private Vector2 direction;
	
	private Vector2 movement;
	// Use this for initialization
	void Start () {
		/*hasSpawn = false;

		GetComponent<Collider2D>().enabled = false;
		// 2 - Check if the enemy has spawned.
		if (hasSpawn == false) {
			if (GetComponent<Renderer> ().IsVisibleFrom (Camera.main)) {
				Spawn ();
			}
		} else 
		{
			// 4 - Out of the camera ? Destroy the game object.
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
		*/
		GameObject player = GameObject.Find ("Player");
		player1 = (PlayerController2)player.GetComponent (typeof(PlayerController2));
		//GameObject parent = player.transform.parent.gameObject.transform.position.x;
		//player1 = (PlayerController2)GameObject.Find ("Player");
		//player x = transform.Find("Player").GetComponent.

		//Debug.Log ("Player X: " + playerx + " Player Y: " + playery);
		//Debug.Log ("Transform parent x: " + player.transform.parent.gameObject.transform.position.x + " Tranform parent y: " + player.transform.parent.gameObject.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (hasSpawn == false)
		{
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			if(player1 != null)
				direction = (player1.transform.position - this.transform.position).normalized;
			movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
			movement *= Time.deltaTime;
			
			transform.Translate (movement);
			// 4 - Out of the camera ? Destroy the game object.
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}

		
	}

	private void Spawn()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		GetComponent<Collider2D>().enabled = true;
	}
}

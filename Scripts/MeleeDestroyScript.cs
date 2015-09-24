using UnityEngine;
using System.Collections;

public class MeleeDestroyScript : MonoBehaviour {
	
	public int damage = 1;
	public bool isEnemyShot = false;
	
	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, 0.8f);
		
	}
	
}
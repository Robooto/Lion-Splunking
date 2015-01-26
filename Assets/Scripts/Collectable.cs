using UnityEngine;
using System.Collections;

/*
 * This class when a object tagged player touches it 
 * it will destory the object
 */

public class Collectable : MonoBehaviour {

	public AudioClip pickupSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// when another sprite enters trigger array do code
	void OnTriggerEnter2D(Collider2D target){
		// if the target is a player destory the trigger
		if (target.gameObject.tag == "Player") {
			if(pickupSound)
				AudioSource.PlayClipAtPoint(pickupSound, transform.position);

			Destroy (gameObject);
		}
	}
}

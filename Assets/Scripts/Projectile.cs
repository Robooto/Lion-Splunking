using UnityEngine;
using System.Collections;

/*
 * Projectile when it collides with something it will destory itself
 */

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Player") {
			var explode = target.gameObject.GetComponent<Explode> () as Explode;
			explode.OnExplode ();
		}
		Destroy (gameObject);
		
	}
}

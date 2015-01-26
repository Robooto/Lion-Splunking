using UnityEngine;
using System.Collections;

/*
 * This script will detect which direction the player is facing for our camera
 */

public class PlayerDirection : MonoBehaviour {

	public bool facingRight = true;


	// Update is called once per frame
	void Update () {
		if(Input.GetAxis ("Horizontal") > 0 && !facingRight){
			facingRight = true;
			transform.rotation = Quaternion.Euler (0,0,0);
		} else if(Input.GetAxis ("Horizontal") < 0 && facingRight) {
			facingRight = false;
			// using quaternion.euler rotataion instead of -1 on the x-axis because it does werid things with colliders
			transform.rotation = Quaternion.Euler (0,200,0);
		}
	}
}

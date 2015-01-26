using UnityEngine;
using System.Collections;

/*
 * Player class is managed by a controller.  The class itself handles how fast the player can move and the animation states.
 */

public class Player : MonoBehaviour {

	// public vars can be edited in the editor
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3, 5);


	public bool standing;
	public float jetSpeed = 15f;
	public float airSpeedMultiplier = .3f;

	public AudioClip leftFootSound;
	public AudioClip rightFootSound;
	public AudioClip thudSound;
	public AudioClip rocketSound;

	private Animator animator;
	private PlayerController controller;

	void Start() {
		// get controller for this sprite
		controller = GetComponent<PlayerController> ();
		// get animator instance for this sprite
		animator = GetComponent<Animator> ();
	}

	void PlayRocketSound(){
		if (!rocketSound || GameObject.Find ("RocketSound"))
			return;

		GameObject go = new GameObject ("RocketSound");
		AudioSource aSrc = go.AddComponent<AudioSource> ();
		aSrc.clip = rocketSound;
		aSrc.volume = 0.7f;
		aSrc.Play ();

		Destroy (go, rocketSound.length);
	}

	void PlayLeftFootSound(){
		if (leftFootSound)
			AudioSource.PlayClipAtPoint (leftFootSound, transform.position);
	}

	void PlayRightFootSound(){
		if (rightFootSound)
			AudioSource.PlayClipAtPoint (rightFootSound, transform.position);
	}

	// play sound when player collides with something
	void OnCollisionEnter2D(Collision2D target){
		if (!standing) {
			var absVelX = Mathf.Abs (rigidbody2D.velocity.x);
			var absVelY = Mathf.Abs (rigidbody2D.velocity.y);

			if(absVelX <= .1f || absVelY <= .1f){
				if(thudSound)
					AudioSource.PlayClipAtPoint(thudSound, transform.position);
			}
		}
	}


	// Update is called once per frame
	void Update () {
		var forceX = 0f;
		var forceY = 0f;

		// gets the velocity from the player.x
		var absVelX = Mathf.Abs (rigidbody2D.velocity.x);

		// gets the velocity from the player.y
		var absVelY = Mathf.Abs (rigidbody2D.velocity.y);

		if (absVelY < .2f) {
				standing = true;
		} else {
				standing = false;
		}

		if (controller.moving.x != 0) {
			if (absVelX < maxVelocity.x) {

				// if we are standing then no air drag
				// if we are in the air then we need air speed drag
				forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x * airSpeedMultiplier);

				// make sure our character is facing the correct way 
				// if forceX is more than 0 face right if not then face left
				transform.localScale = new Vector3 (forceX > 0 ? 1 : -1, 1, 1);
			}
				// play the walk animation if we are moving
				animator.SetInteger ("AnimState", 1);
			} else {
				// if we aren't moving play the idle animation
				animator.SetInteger("AnimState", 0);
			}

		if (controller.moving.y > 0) {
			PlayRocketSound();
			if (absVelY < maxVelocity.y)
					forceY = jetSpeed * controller.moving.y;

				// if we are in the air then play jet animation
				animator.SetInteger ("AnimState", 2);
			} else if (absVelY > 0) {
				// if we aren't accellerating then play emptyp animation
				animator.SetInteger ("AnimState", 3);	
			}

		rigidbody2D.AddForce (new Vector2(forceX, forceY));
	}
}

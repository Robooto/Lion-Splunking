using UnityEngine;
using System.Collections;

/*
 * Player controller scripted with double jump!
 */

public class PlayerController2D : MonoBehaviour {
	
	//Character movement based variables
	public float mySpeed;
	private float moveDirection;
	
	//Character jump based variables
	public float jumpPower;
	private bool playerJumped;
	private bool doubleJumped;
	
	public bool doubleJump;
	
	//Character ground-check based var
	public Transform groundChecker; //Gameobject required, place where you wish ground to be dtected from
	private bool isGrounded;
	
	//Character Misc var
	private bool facingRight = true;
	
	// animation stuff
	private Animator animator;

	//AudioStuff
	public AudioClip leftFootSound;
	public AudioClip rightFootSound;
	public AudioClip jumpSound;
	
	void Start() {
		// get animator instance for this sprite
		animator = GetComponent<Animator> ();
	}


	void PlayLeftFootSound(){
		if (leftFootSound)
			AudioSource.PlayClipAtPoint (leftFootSound, transform.position);
	}
	
	void PlayRightFootSound(){
		if (rightFootSound)
			AudioSource.PlayClipAtPoint (rightFootSound, transform.position);
	}
	
	
	// Update is called once per frame
	void Update () {
		//linecast downards towards t he ground to see if we are on the ground
		isGrounded = Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
		
		//if we are grounded
		if (isGrounded) {
			doubleJumped = false;
		}
		
		//If we hit jump and are grounded
		if (Input.GetButtonDown ("Jump") && isGrounded) {
			playerJumped = true;
		}
		
		//if we aren't grounded check it we have doubled jumped
		else if (Input.GetButtonDown ("Jump") && !doubleJumped) {
			if(!doubleJump){
				return;
			}
			doubleJumped = true;
			playerJumped = true;
		}
		
	}
	
	void FixedUpdate (){
		//Note: Physics should only be adjusted in FixedUpdate in order to allow for smooth physics
		
		//Grab our moving direction (-1 is left, 1 is right)
		//Go into Edit > Project Settings > Input Manager and change the sensitivity / gravity values
		// on the "Horizontal" button for loose (lower numbers) or tight (try 1000) controls.
		moveDirection = Input.GetAxis("Horizontal");
		
		//Move our player left/right based on which horizontal axis they are hitting (always returns -1, 0, or 1,
		// then multiply those values by mySpeed and Time.deltaTime. Set the Y velocity to the current players Y velocity,
		// meaning we don't really touch it at all (ie; jumping/falling gets left alone.
		rigidbody2D.velocity = new Vector2(moveDirection * mySpeed * Time.deltaTime,rigidbody2D.velocity.y);
		
		if (Mathf.Abs (rigidbody2D.velocity.x) == 0 && isGrounded) {
			// if we aren't moving play the idle animation
			animator.SetInteger("AnimState", 0);
			
		} else if (Mathf.Abs (rigidbody2D.velocity.x) > 0 && isGrounded) {
			animator.SetInteger ("AnimState", 1);
		} else {
			animator.SetInteger("AnimState", 2);
		}
		
		
		//If our player has been allowed to jump, then...
		if (playerJumped){
			playerJumped = false; //Set Player Jumped to False
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,jumpPower); //Give us some upward force
			if(jumpSound)
				AudioSource.PlayClipAtPoint(jumpSound, transform.position);
		
		}
		
		//Flip our character around if they are not facing the correct direction
		if (moveDirection > 0 && !facingRight){
			FlipCharacter();
		}else if (moveDirection < 0 && facingRight){
			FlipCharacter();
		}
	}
	
	//Simple script to flip our character around. If we did scale x = -1, it would do weird things with the collider
	void FlipCharacter(){
		facingRight = !facingRight;
		
		if(facingRight){
			transform.rotation = Quaternion.Euler(0,0,0);
		}
		else{
			transform.rotation = Quaternion.Euler(0,180,0);
		}
	}
}

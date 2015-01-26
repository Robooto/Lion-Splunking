using UnityEngine;
using System.Collections;

/*
 * This class will attach when a player enters the trigger area and kill the player if it stays in the trigger area.  
 * If the player quickly leaves the player will live
 */

public class AlienB : MonoBehaviour {
	
	private Animator animator;
	
	private bool readyToAttack;

	public AudioClip attackSound;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			if(readyToAttack){
				//var explode = target.GetComponent<Explode>() as Explode;
				//explode.OnExplode();
				
			}else{
				animator.SetInteger ("AnimState", 1);
				if(attackSound)
					AudioSource.PlayClipAtPoint(attackSound, transform.position);
			}
		}
	}

	void OnTriggerStay2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			if(readyToAttack){
				var explode = target.GetComponent<Explode>() as Explode;
				explode.OnExplode();
				
			}else{
				animator.SetInteger ("AnimState", 1);
				if(attackSound)
					AudioSource.PlayClipAtPoint(attackSound, transform.position);
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D target){
		readyToAttack = false;
		animator.SetInteger ("AnimState", 0);
	}
	
	void Attack(){
		readyToAttack = true;
	}

	void StopAttack(){
		readyToAttack = false;
	}
}

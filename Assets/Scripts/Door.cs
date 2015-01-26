using UnityEngine;
using System.Collections;

/*
 * Door class.  Manages the opening and closing animations of the door.  As the door opens we remove the collider2D and as the door
 * close we add back the collider2d.  The door is operated by a door trigger.
 */

public class Door : MonoBehaviour {

	public const int IDLE = 0;
	public const int OPENING = 1;
	public const int OPEN = 2;
	public const int CLOSING = 3;
	public float closeDelay = .5f;

	private int state = IDLE;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// these methods are just for knowing where the door is in its animations
	void OnOpenStart(){
		state = OPENING;
	}
	void OnOpenEnd(){
		state = OPEN;
	}

	void OnCloseStart(){
		state = CLOSING;
	}
	void OnCloseEnd(){
		state = IDLE;
	}

	// let the player pass through the door
	void DissableCollider2D() {
		collider2D.enabled = false;
	}

	// stop the player from passing through the door
	void EnableCollider2D() {
		collider2D.enabled = true;
	}

	public void Open() {
		animator.SetInteger ("AnimState", 1);
	}

	public void Close() {
		StartCoroutine (CloseNow ());
	}

	private IEnumerator CloseNow(){
		yield return new WaitForSeconds(closeDelay);
		animator.SetInteger ("AnimState", 2);
	}

}

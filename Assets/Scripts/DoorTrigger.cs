using UnityEngine;
using System.Collections;

/*
 * This class manages our doors open and close events.  When the player enters the trigger area it will open or close the door.
 * The trigger can be ignored if this door is connected to a switch.  There is a gizmo that display the area of the trigger too.
 */

public class DoorTrigger : MonoBehaviour {

	public Door door;

	public bool ignoreTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){

		if (ignoreTrigger)
			return;

		if (target.gameObject.tag == "Player") {
			door.Open ();
		}
	}

	void OnTriggerExit2D(Collider2D target){

		if (ignoreTrigger)
			return;

		if (target.gameObject.tag == "Player") {
			door.Close ();
		}
	}

	public void Toggle(bool value) {
		if (value)
			door.Open ();
		else
			door.Close ();
	}

	void OnDrawGizmos() {
		Gizmos.color = ignoreTrigger ? Color.gray : Color.green;

		var bc2d = GetComponent<BoxCollider2D> ();
		var bc2dPos = bc2d.transform.position;
		var newPos = new Vector2 (bc2dPos.x + bc2d.center.x, bc2dPos.y + bc2d.center.y);
		Gizmos.DrawWireCube(newPos, new Vector2(bc2d.size.x, bc2d.size.y));
	}
}

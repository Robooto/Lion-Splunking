using UnityEngine;
using System.Collections;

/*
 * This class is for a switch that can be attached to doors.  It can also be made sticky. Which means it can only be set off.
 * When anything touches the trigger it will send a toggle to our door trigger class
 * There is also a gismo that shows what door triggers the switch controls
 */

public class Switch : MonoBehaviour {

	private Animator animator;
	private bool down;

	// attach this to doors
	public DoorTrigger[] doorTriggers;
	// makes the switch stick when something is placed on it
	public bool sticky;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		animator.SetInteger ("AnimState", 1);
		down = true;

		// this will open all doors this switch is attached to
		foreach (DoorTrigger trigger in doorTriggers) {
			if(trigger != null)
				trigger.Toggle(true);
		}
	}

	void OnTriggerExit2D(Collider2D target){
		if (sticky && down)
			return;

		animator.SetInteger ("AnimState", 2);
		down = false;

		foreach (DoorTrigger trigger in doorTriggers) {
			if(trigger != null)
				trigger.Toggle(false);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = sticky ? Color.red : Color.green;
		foreach (DoorTrigger trigger in doorTriggers) {
			if(trigger != null)
				Gizmos.DrawLine (transform.position, trigger.door.transform.position);
		}
	}
}

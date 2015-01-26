using UnityEngine;
using System.Collections;

/*
 * This class adds a line of sight to an object.  If it collides with a 'solid' layer it will switch directions.
 * It can also detect if it reaches the end of a solid object and turn around
 */

public class LookFoward : MonoBehaviour {

	public Transform sightStart, sightEnd;
	public bool needsCollision = true;

	private bool collision = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// create a line of sight that when touch es a layer called solid turns true
		collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);

		// if collision happens make the object turn around
		if (collision == needsCollision) {
			this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);
		}
	}
}

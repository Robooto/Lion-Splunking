using UnityEngine;
using System.Collections;

/*
 * This class will destory objects that touch a object that is tagged dealy
 * and then will spawn a sprite that is attached to it
 */

public class Explode : MonoBehaviour {

	public BodyPart bodyPart;
	public int totalParts = 5;

	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// when object enters a tagged deadly object destory it
	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Deadly") {
			OnExplode();
		}
	}

	// when object collides with a tagged deadly object destory it
	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Deadly") {
			OnExplode();
		}
	}

	public void OnExplode(){
		Destroy (gameObject);

		if(deathSound)
			AudioSource.PlayClipAtPoint (deathSound, transform.position);

		var t = transform;

		for (int i = 0; i < totalParts; i++) {
			t.TransformPoint (0, -100, 0);
			BodyPart clone = Instantiate(bodyPart, t.position, Quaternion.identity) as BodyPart;
			clone.rigidbody2D.AddForce(Vector3.right * Random.Range (-50, 50));
			clone.rigidbody2D.AddForce(Vector3.up * Random.Range (100, 400));
		}

		// click loads the scene that is current loaded using a class we already made
		GameObject go = new GameObject ("ClickToContinue");
		ClickToContinue script = go.AddComponent<ClickToContinue> ();
		script.scene = Application.loadedLevelName;
		go.AddComponent<DisplayRestartText> ();
	}
}

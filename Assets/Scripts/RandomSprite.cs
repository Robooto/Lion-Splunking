using UnityEngine;
using System.Collections;

/*
 * This class will select a random sprite from a sprite sheet 
 */

public class RandomSprite : MonoBehaviour {

	public Sprite[] sprites;
	public string resourceName;

	// make sure we don't pick the same one twice
	public int currentSprite = -1;
	// Use this for initialization
	void Start () {
		if (resourceName != "") {
			sprites = Resources.LoadAll<Sprite> (resourceName);

			if(currentSprite == -1){
				currentSprite = Random.Range (0, sprites.Length);
			} else if (currentSprite > sprites.Length) {
				currentSprite = sprites.Length - 1;
			}
			GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

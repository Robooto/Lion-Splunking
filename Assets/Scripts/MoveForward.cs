﻿using UnityEngine;
using System.Collections;

/*
 * Moves a sprite forward at a constant speed.
 */

public class MoveForward : MonoBehaviour {

	public float speed = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector2 (transform.localScale.x, 0) * speed;
	
	}
}

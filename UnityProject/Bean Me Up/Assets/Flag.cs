﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

	public Sprite redFlag;
	public Sprite greenFlag;

	public bool activated = false;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){


		if (col.tag == "Player" && !activated) {
			activated = true;
			GetComponent<SpriteRenderer> ().sprite = greenFlag;
            GetComponent<Checkpoint>().fuelSaved = col.GetComponent<controller>().fuelCollected;
			GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().playerCheckpoint = gameObject.GetComponent<Checkpoint> ();
		
		}

	}
}

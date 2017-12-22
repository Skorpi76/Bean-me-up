using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyHole : MonoBehaviour {

	public GameObject key;
	public bool Open = false;

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject == key) {
			 Open = true;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject == key) {
			Open = false;
		}
	}
}

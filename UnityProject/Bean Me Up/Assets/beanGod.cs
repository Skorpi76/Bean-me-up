using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beanGod : MonoBehaviour {

	public GameObject GameOverButton;
	GameObject buttonInstance;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){

		if (col.tag == "Player") {
			if (buttonInstance == null) {
				buttonInstance = Instantiate (GameOverButton) as GameObject;
			}
		}

	}

	void OnTriggerExit2D(Collider2D col){

		if (col.tag == "Player") {
			if (buttonInstance != null) {
				Destroy(buttonInstance);
			}
		}

	}
}

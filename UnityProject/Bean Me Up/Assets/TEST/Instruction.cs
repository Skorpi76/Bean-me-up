using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instruction : MonoBehaviour {

	bool activated = false;
	public bool StartActive = false;

	// Use this for initialization
	void Start () {
		if (StartActive) {
			Color col = GetComponent<TextMeshPro> ().color;
			col.a = 1;
			GetComponent<TextMeshPro> ().color = col;
		} else {
			Color col = GetComponent<TextMeshPro> ().color;
			col.a = 0;
			GetComponent<TextMeshPro> ().color = col;
		}
	}

	IEnumerator FadeIn(){
		float t = 0;
		while (t <= 1) {
			t += Time.deltaTime;
			Color col = GetComponent<TextMeshPro> ().color;
			col.a = t;
			GetComponent<TextMeshPro> ().color = col;
			yield return null;
		}
		if (!activated) {
			StartCoroutine (FadeOut ());
		}
	}

	IEnumerator FadeOut(){
		float t = 1;
		while (t >= 0) {
			t -= Time.deltaTime;
			Color col = GetComponent<TextMeshPro> ().color;
			col.a = t;
			GetComponent<TextMeshPro> ().color = col;
			yield return null;
		}
		if (activated) {
			StartCoroutine (FadeIn ());
		}
	}

	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D col) {

		if (col.tag == "Player") {
			activated = true;
			StartCoroutine (FadeIn ());
		}

	}

	void OnTriggerExit2D (Collider2D col) {

		if (col.tag == "Player") {
			activated = false;
			StartCoroutine (FadeOut ());
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slugMovement : MonoBehaviour {

	Vector3 dir;
	Rigidbody2D rb;
	public LayerMask inchLayerMask;
	public float inchAmount = 400;
	public float waitAmount = 3;

	// Use this for initialization
	void Start () {
		dir = new Vector3(1,0,0);
		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine (InchMove ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InchAgain(){
		GetComponent<Animator> ().SetTrigger ("Slide");
		StartCoroutine (InchMove ());
	}

	IEnumerator InchMove(){
		rb.AddForce (transform.right * dir.x * inchAmount);
		yield return new WaitForSeconds (waitAmount);
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right * dir.x, 0.7f, inchLayerMask);
		if (hit.collider != null) {
			print ("hit a wall");
			dir.x = dir.x * -1;
			GetComponent<SpriteRenderer> ().flipX = !GetComponent<SpriteRenderer> ().flipX;
		}
		InchAgain ();
	}
}

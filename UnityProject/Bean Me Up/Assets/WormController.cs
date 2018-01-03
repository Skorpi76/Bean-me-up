using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour {

	Rigidbody2D rb;
	public float waitAmount;
	public float moveAmount;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator Move(){
		//rb.AddForce (transform.right * dir.x * moveAmount);
		yield return new WaitForSeconds (waitAmount);
		//RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right * dir.x, 0.7f, inchLayerMask);
		//if (hit.collider != null) {
		//	print ("hit a wall");
			//dir.x = dir.x * -1;
		//	GetComponent<SpriteRenderer> ().flipX = !GetComponent<SpriteRenderer> ().flipX;
		//}
		//InchAgain ();
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour {

	Rigidbody2D rb;
	Animator anim;
	public float shrinkTime;
	public float moveTime;
	public float moveAmount;
	public float pursueDistance = 10;

	Quaternion targetRotation;
	bool hasMoved = false;

	IEnumerator mover;

	public float damageAmount;

	// Use this for initialization
	void Start () {

		mover = Move ();
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		MoveAgain ();
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Ship" || col.collider.tag == "Player") {
			col.collider.GetComponent<Entity> ().ModifyHealth (-25);
		}
	}

	void Update(){
		

		if (targetRotation.x != 0 || targetRotation.y != 0 || targetRotation.z != 0 || targetRotation.w != 0) {
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, 15 * Time.deltaTime);
		}

	}

	// Update is called once per frame
	void MoveAgain () {
		
		StartCoroutine (Move ());
	}


	IEnumerator Move(){
		//rb.AddForce (transform.right * dir.x * moveAmount);
		yield return new WaitForSeconds (shrinkTime);
		GameObject player = GameObject.FindGameObjectWithTag ("Ship");
		if (!player.GetComponent<SpaceShipMovement1> ().piloted) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		float distance = Vector3.Distance (transform.position, player.transform.position);
		if (distance<= pursueDistance) {
			//pursue

			bool returnStart = false;
			if (player.GetComponent<SpaceShipMovement1> ()) {
				if (player.GetComponent<SpaceShipMovement1> ().gravityPull != Vector3.zero) {
					returnStart = true;
				}
			}if (player.GetComponent<controller> ()) {
				if (!player.GetComponent<controller> ().inSpace) {
					returnStart = true;
				}
			}

			if(returnStart){
				//move
				Vector3 direction = GetComponent<SpawnController>().Respawner.transform.position - transform.position; //direction to player
				rb.AddForce (direction.normalized * moveAmount);

				//rotate
				float rot_z = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
				targetRotation = Quaternion.Euler (0f, 0f, rot_z);
				yield return new WaitForSeconds (moveTime);
				anim.SetTrigger ("Move");

			} else {

				Debug.Log ("Pursue");

				//move
				Vector3 direction = player.transform.position - transform.position; //direction to player
				rb.AddForce (direction.normalized * moveAmount);
		
				//rotate
				float rot_z = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
				targetRotation = Quaternion.Euler (0f, 0f, rot_z);
				yield return new WaitForSeconds (moveTime);
				anim.SetTrigger ("Move");
			}
			//transform.rotation = Quaternion.Lerp(transform.rotation, , Time.deltaTime * 10);
		}else{

//			float x = Random.Range (-1, 1);
//			float y = Random.Range (-1, 1);
//
//			Vector3 direction = new Vector3 ( x, y, 0);
//			rb.AddForce (direction.normalized * moveAmount);
//
//			float rot_z = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
//			targetRotation = Quaternion.Euler (0f, 0f, rot_z );
//			yield return new WaitForSeconds (moveTime);
//			anim.SetTrigger ("Move");

		}

		hasMoved = true;
		MoveAgain ();
	}


}

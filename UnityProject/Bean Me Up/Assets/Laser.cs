using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Ship" || coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<Entity> ().ModifyHealth (-20);
			coll.gameObject.GetComponent<Rigidbody2D> ().AddForce( Vector3.Reflect (coll.gameObject.GetComponent<Rigidbody2D> ().velocity, coll.contacts [0].normal) * 100);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}

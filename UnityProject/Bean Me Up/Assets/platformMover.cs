using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMover : MonoBehaviour {

	Vector3 startPos;
	public Transform endPosition;
	public float speed;
	Vector3 targetPos;


	void Start () {
	
		startPos = transform.position;	
		targetPos = endPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = Vector3.MoveTowards (transform.position, targetPos, Time.deltaTime * speed);
		if(Vector3.Distance(transform.position, targetPos) < 0.2f){
			if (targetPos == endPosition.position) {
				targetPos = startPos;
			} else {
				targetPos = endPosition.position;
			}
		}
	}
		
}

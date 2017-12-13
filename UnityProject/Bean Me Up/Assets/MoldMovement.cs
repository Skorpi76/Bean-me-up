using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldMovement : MonoBehaviour {


	public float fireRate;
	public GameObject moldProjectile;
	public Vector3 targetPosition;
	float rotationTarget;
	Rigidbody2D rb;

	Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
		rb = GetComponent<Rigidbody2D> ();
        Shoot();
		StartCoroutine (Movement ());
	}

	void Shoot(){
		Vector3[] shootDirections = {
			transform.up,
			transform.up * -1,
			transform.right,
			transform.right * -1,
			transform.up + transform.right,
			transform.up + transform.right * -1,
			transform.up * -1 + transform.right,
			transform.up * -1 + transform.right * -1,
		};
		foreach (Vector3 dir in shootDirections) {
            Quaternion rot = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90, Vector3.forward);
			GameObject proj = Instantiate(moldProjectile, transform.position + dir.normalized * 1.5f, rot) as GameObject;
            proj.GetComponent<Rigidbody2D>().velocity = proj.transform.up * -3;
		}
		StartCoroutine (ShootDelay ());
	}

	IEnumerator ShootDelay(){

		yield return new WaitForSeconds (fireRate);
		Shoot ();

	}

	IEnumerator Movement(){
		float x = Random.Range (-5, 5);
		float y = Random.Range (-5, 5);
		targetPosition = new Vector3 (startingPosition.x + x, startingPosition.y + y, startingPosition.z);

		rotationTarget = Random.Range (-10, 10);

		yield return new WaitForSeconds (3);
		StartCoroutine (Movement ());

	}
	
	// Update is called once per frame
	void Update () {
		rb.MovePosition (Vector3.MoveTowards(transform.position, targetPosition,Time.deltaTime * 2));
		transform.Rotate (0, 0, rotationTarget * Time.deltaTime);
	}
}

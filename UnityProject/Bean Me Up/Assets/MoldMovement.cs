using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldMovement : MonoBehaviour {

	public Vector3[] shootDirections;
	public float fireRate;
	public GameObject moldProjectile;

	// Use this for initialization
	void Start () {
        Shoot();
	}

	void Shoot(){
		foreach (Vector3 dir in shootDirections) {
            Quaternion rot = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90, Vector3.forward);
            GameObject proj = Instantiate(moldProjectile, transform.position + dir, rot) as GameObject;
            proj.GetComponent<Rigidbody2D>().velocity = proj.transform.up * -3;
		}
		StartCoroutine (ShootDelay ());
	}

	IEnumerator ShootDelay(){

		yield return new WaitForSeconds (5);
		Shoot ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

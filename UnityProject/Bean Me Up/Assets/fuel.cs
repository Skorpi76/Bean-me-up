using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuel : MonoBehaviour {


	
	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.tag == "Player") {
            print("Collect Fuel");
            col.collider.GetComponent<controller>().fuelCollected += 20;
            Destroy(gameObject);
        }
	}
}

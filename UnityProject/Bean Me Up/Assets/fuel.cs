using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fuel : MonoBehaviour {


	
	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.tag == "Player") {
            print("Collect Fuel");
			col.collider.GetComponent<controller>().fuelCollected += 20;
			col.transform.Find ("HealthCanvas").Find ("Slider (1)").gameObject.GetComponent<Slider> ().value = col.collider.GetComponent<controller>().fuelCollected;
            Destroy(gameObject);
        }
	}
}

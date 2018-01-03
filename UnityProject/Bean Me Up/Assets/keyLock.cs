using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyLock : MonoBehaviour {

    public keyHole[] keyHoles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool anyInactive = false;
		foreach (keyHole kh in keyHoles) {
			if (!kh.Open) {
				anyInactive = true;
				break;
			}
		}

		if (!anyInactive) {
			Destroy (gameObject);
		} else {
		
		}
	}
}

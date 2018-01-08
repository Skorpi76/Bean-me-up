using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {

    public Color red;
    public Color green;

    public bool activated = false;


	void Start(){

		GetComponent<Checkpoint> ().ringScript = this;
		foreach (Transform t in transform) {
			if (t.tag != "Map") {
				t.GetComponent<SpriteRenderer> ().color = red;
			}

		}

	}


	public void Deactivate(){
		activated = false;
		foreach (Transform t in transform) {
			if (t.tag != "Map") {
				t.GetComponent<SpriteRenderer> ().color = red;
			}

		}

	}

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D col)
    {


        if (col.tag == "Ship" && !activated)
        {
            activated = true;

			foreach (Transform t in transform) {
				if (t.tag != "Map") {
					t.GetComponent<SpriteRenderer> ().color = green;
				}

			}

			if (GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint.GetComponent<Ring> ()) {
				GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint.GetComponent<Ring> ().Deactivate ();
			}
            GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint = gameObject.GetComponent<Checkpoint>();

        }

    }
}

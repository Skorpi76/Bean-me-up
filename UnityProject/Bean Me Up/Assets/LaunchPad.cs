using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchPad : MonoBehaviour {


	public Object level;

	GameObject ship;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){

		if (col.tag == "Ship") {
			ship = col.gameObject;
			ship.GetComponent<SpaceShipMovement1> ().launchPad = this;
		}
	}

	void OnTriggerExit2D(Collider2D col){

		if (col.tag == "Ship") {
			ship = null;
			ship.GetComponent<SpaceShipMovement1> ().launchPad = null;
		}
	}

	public void ExitPlanet(){

		UnLoadLevel ();

	}

	public void EnterPlanet(){
		ship.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
		ship.transform.position = transform.position;
		ship.transform.rotation = transform.rotation;
		ship.GetComponent<SpaceShipMovement1> ().landed = true;

		LoadLevel ();
	}

	void LoadLevel(){

		SceneManager.LoadScene(level.name, LoadSceneMode.Additive);

	}

	//once the player exits the ship lock the ship in place 
	void LockShipPosition(){
	}

	void UnLoadLevel(){

		SceneManager.UnloadSceneAsync(level.name);

	}

	// Update is called once per frame
	void Update () {
		
	}
}

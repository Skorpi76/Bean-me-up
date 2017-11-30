using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchPad : MonoBehaviour {


	//public Object level;
	public string LevelName;
	public GameObject ship;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){


		if (col.tag == "Ship") {
			ship = col.gameObject;

			ship.GetComponent<SpaceShipMovement1> ().launchPad = gameObject;


		}
	}

	void OnTriggerExit2D(Collider2D col){

		if (col.tag == "Ship") {
			if(col.GetComponent<SpaceShipMovement1>().landed == false){
				ship.GetComponent<SpaceShipMovement1> ().launchPad = null; //
				ship = null;
			}

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
		ship.GetComponent<SpaceShipMovement1> ().launchPad = gameObject;

		LoadLevel ();
		GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().playerCheckpoint = gameObject.GetComponent<Checkpoint> ();
	}

	void LoadLevel(){

		SceneManager.LoadScene(LevelName, LoadSceneMode.Additive);

	}

	//once the player exits the ship lock the ship in place 
	void LockShipPosition(){
	}

	void UnLoadLevel(){

		SceneManager.UnloadSceneAsync(LevelName);

	}

	// Update is called once per frame
	void Update () {
		
	}
}

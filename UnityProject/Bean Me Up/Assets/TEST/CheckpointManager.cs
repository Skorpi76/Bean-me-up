using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

	public Checkpoint playerCheckpoint;
	public Checkpoint shipCheckpoint;

	public GameObject playerPrefab;
	public GameObject shipPrefab;

	// Use this for initialization
	void Start () {
		
	}


	public void RespawnPlayer(){
		//destroy player
		GameObject[] player = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject p in player) {
			Destroy (p);
		}

		playerCheckpoint.Respawn ();

	}

	public void RespawnShip(){}

	public void NewPlayerCheckpoint(){}

	public void NewShipCheckpoint(){}
	
	// Update is called once per frame
	void Update () {
		
	}
}

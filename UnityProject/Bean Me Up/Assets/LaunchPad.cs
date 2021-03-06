﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchPad : MonoBehaviour
{


    //public Object level;
    public string LevelName;
    public GameObject ship;
    public Checkpoint shipCheckpoint;
    public Checkpoint playerCheckpoint;
    public bool visited = false;

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D col)
    {


        if (col.tag == "Ship")
        {
            ship = col.gameObject;

            ship.GetComponent<SpaceShipMovement1>().launchPad = gameObject;


        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "Ship")
        {
            if (col.GetComponent<SpaceShipMovement1>().landed == false)
            {
                ship.GetComponent<SpaceShipMovement1>().launchPad = null; //
                ship = null;
            }

        }
    }

    public void ExitPlanet()
    {
        UnLoadLevel();
		if(GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint != null){
			if (GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint.GetComponent<Ring> ()) {
				GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint.GetComponent<Ring> ().Deactivate ();
			}
		}
        GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().shipCheckpoint = shipCheckpoint;

        Booster[] boosters = GameObject.FindObjectsOfType<Booster>();
        foreach (Booster b in boosters)
        {
            if (b.destroyOnPlanetLeave)
            {
                Destroy(b.gameObject);
            }
        }

		//PrefabSpawner[] spawners = GameObject.FindObjectsOfType<PrefabSpawner>();
		//foreach (PrefabSpawner ps in spawners)
		//{
		//	ps.Spawn ();
		//}
    }

    public void EnterPlanet()
    {
        ship.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        ship.transform.position = transform.position;
        ship.transform.rotation = transform.rotation;
        ship.GetComponent<SpaceShipMovement1>().landed = true;
        ship.GetComponent<SpaceShipMovement1>().launchPad = gameObject;

        LoadLevel();

		if(GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint != null){
			if (GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint.GetComponent<Ring> ()) {
				GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().shipCheckpoint.GetComponent<Ring> ().Deactivate ();
			}
		}
        GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().shipCheckpoint = shipCheckpoint;


        GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().playerCheckpoint = playerCheckpoint;

        if (visited == false)
        {
            visited = true;
            Camera.main.GetComponent<QuestLogController>().NewPlanet();
        }
    }

    void LoadLevel()
    {

        SceneManager.LoadScene(LevelName, LoadSceneMode.Additive);

    }

    //once the player exits the ship lock the ship in place 
    void LockShipPosition()
    {
    }

    void UnLoadLevel()
    {

        SceneManager.UnloadSceneAsync(LevelName);

    }

    // Update is called once per frame
    void Update()
    {

    }
}

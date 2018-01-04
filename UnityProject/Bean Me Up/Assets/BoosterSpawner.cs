using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour {

	public int boosterId;
	public GameObject boosterPrefab;
    public bool destroyOnPlanetUnload = false;
    // Use this for initialization
    void Start () {
		Spawn();
	}


	void Spawn(){

		if (Camera.main.GetComponent<QuestLogController> ().boosterCollected [boosterId] == false) {
			Debug.Log ("SpawnBooster");
			GameObject boosterReference = Instantiate (boosterPrefab,transform.position,transform.rotation) as GameObject;
			boosterReference.GetComponent<Booster> ().ID = boosterId;
            boosterReference.GetComponent<Booster>().destroyOnPlanetLeave = destroyOnPlanetUnload;
        }

	}

	// Update is called once per frame
	void Update () {
		
	}
}

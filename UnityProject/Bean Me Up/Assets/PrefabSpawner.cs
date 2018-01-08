using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

	public GameObject prefab;
	public float respawntime = 90;
	GameObject prefabInstance;


	// Use this for initialization
	void Start () {
		Spawn ();
	}

	public void Spawn(){
		if (prefabInstance != null) {
			Destroy (prefabInstance);
		}
		prefabInstance = Instantiate (prefab, transform.position, transform.rotation) as GameObject;
		prefabInstance.GetComponent<SpawnController> ().Respawner = this;
	}

	public void StartRespawn(){
		StartCoroutine (RespawnTimer ());
	}

	IEnumerator RespawnTimer(){
		yield return new WaitForSeconds(respawntime);
		Spawn ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}

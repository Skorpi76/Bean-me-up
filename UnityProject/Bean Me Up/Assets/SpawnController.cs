using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	public PrefabSpawner Respawner;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void OnDestroy()
    {
        if (Respawner != null)
        {
            Respawner.StartRespawn();
        }
    }
}

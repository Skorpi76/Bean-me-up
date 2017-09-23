using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShoot : MonoBehaviour {
    private float nextFire = 0.3f; // how often the player can shoot
    private float myTime = 0.0f; // Var to hold our time 
    public GameObject projectile;
    public Transform projectileSpawn;
    

	
	// Update is called once per frame
	void Update () {
        myTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && myTime > nextFire)
        {
            Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
            myTime = 0.0f;
        }
    }
}

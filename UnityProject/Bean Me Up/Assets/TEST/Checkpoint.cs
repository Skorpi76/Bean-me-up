using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {


	public PlayerRespawnMode pRespawnMode;
	public RespawnType spawnPlayerType;
    public float fuelSaved = 0;
	public Ring ringScript;

	// Use this for initialization
	public void Respawn(){
        switch (spawnPlayerType) {
            case RespawnType.Ship:
                GameObject ship = GameObject.FindGameObjectWithTag("Ship");
                ship.transform.position = transform.position;
                ship.transform.rotation = transform.rotation;
                ship.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                ship.GetComponent<SpaceShipMovement1>().fuel = 100;
                ship.GetComponent<Entity>().ResetHealth();
                break;
            case RespawnType.Player:

                switch (pRespawnMode)
                {
                    case PlayerRespawnMode.InShip:
                        GetComponent<SpaceShoot>().enabled = true;
                        GetComponent<SpaceShipMovement1>().piloted = true;
                        GetComponent<SpaceShipMovement1>().Canvas.SetActive(true);
                        Camera.main.GetComponent<CameraFollowSpaceShip>().followPlayer = false;
                        break;
                    case PlayerRespawnMode.AtPosition:
                        Camera.main.GetComponent<CameraFollowSpaceShip>().followPlayer = true;
                        GameObject player = Instantiate(GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().playerPrefab, transform.position + transform.right, transform.rotation) as GameObject;
                        player.GetComponent<controller>().ship = Camera.main.GetComponent<CameraFollowSpaceShip>().ship;
                        player.GetComponent<controller>().fuelCollected = fuelSaved;
                        Camera.main.GetComponent<CameraFollowSpaceShip>().player = player;
                        break;
                }

                break;
        }
		
	}





}

public enum RespawnType{
	Ship, Player
}

public enum PlayerRespawnMode{
	AtPosition,
	InShip
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {


	public PlayerRespawnMode pRespawnMode;
	public Respawn respawn;
    public float fuelSaved = 0;

	// Use this for initialization
	public void Respawn(){
		switch (pRespawnMode) {
		case PlayerRespawnMode.InShip:
			GetComponent<SpaceShoot> ().enabled = true;
			GetComponent<SpaceShipMovement1> ().piloted = true;
			Camera.main.GetComponent<CameraFollowSpaceShip> ().followPlayer = false;
			break;
		case PlayerRespawnMode.AtPosition:
			Camera.main.GetComponent<CameraFollowSpaceShip> ().followPlayer = true;
			GameObject player = Instantiate (GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().playerPrefab, transform.position + transform.right, transform.rotation) as GameObject;
			player.GetComponent<controller> ().ship = Camera.main.GetComponent<CameraFollowSpaceShip> ().ship;
            player.GetComponent<controller>().fuelCollected = fuelSaved;
            Camera.main.GetComponent<CameraFollowSpaceShip> ().player = player;
			break;
		}
	}





}

public enum Respawn{
	Ship, Player
}

public enum PlayerRespawnMode{
	AtPosition,
	InShip
}
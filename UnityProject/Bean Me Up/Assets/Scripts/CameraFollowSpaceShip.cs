using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSpaceShip : MonoBehaviour {

    public GameObject ship;
	public GameObject player;

	public bool playerInSpace = true;

    [HideInInspector]
    public bool followPlayer = false;
	public Camera mapCamera;
    public float planetZoom;
	public Transform spaceBG;

    // Use this for initialization
    void Start()
    {
        //ship = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey ("m")) {
			if (!mapCamera.enabled) {
				mapCamera.enabled = true;
			}
		} else {
			if (mapCamera.enabled) {
				mapCamera.enabled = false;
			}
		}

                
        if (followPlayer)
        {
            FollowPlayer();
        }
        else {
            FollowShip();
        }
    }

    void FollowPlayer()
    {
		if (!playerInSpace) {
			if (Camera.main.orthographicSize != planetZoom) {
				Camera.main.orthographicSize = Mathf.SmoothStep (Camera.main.orthographicSize, planetZoom, 5 * Time.deltaTime);
			}

			if (transform.rotation != player.transform.rotation) {
				transform.rotation = Quaternion.Lerp (transform.rotation, player.transform.rotation, Time.deltaTime * 10);
			}
		} else {
			if (Camera.main.orthographicSize != 20)
			{
				Camera.main.orthographicSize = Mathf.SmoothStep(Camera.main.orthographicSize, 20, 5 * Time.deltaTime);
			}
			if (transform.rotation != Quaternion.identity)
			{
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10);
			}
		}
		Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;

    }

    void FollowShip()
    {
        if (Camera.main.orthographicSize != 20)
        {
            Camera.main.orthographicSize = Mathf.SmoothStep(Camera.main.orthographicSize, 20, 5 * Time.deltaTime);
        }

		if (transform.rotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10);
        }
        Vector3 newPos = new Vector3(ship.transform.position.x, ship.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;
    }


    public void SetFollowPlayer(bool val)
    {
        followPlayer = val;
    }
}

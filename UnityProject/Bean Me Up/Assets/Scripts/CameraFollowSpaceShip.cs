using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSpaceShip : MonoBehaviour {

    public GameObject ship;
    [HideInInspector]
    public bool followPlayer = false;

    public float planetZoom;

    // Use this for initialization
    void Start()
    {
        //ship = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
                
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
        if (Camera.main.orthographicSize != planetZoom) {
            Camera.main.orthographicSize = Mathf.SmoothStep(Camera.main.orthographicSize, planetZoom, 5 * Time.deltaTime);
        }

        if (transform.rotation != ship.GetComponent<SpaceShipMovement>().playerInstance.transform.rotation) {
            transform.rotation = Quaternion.Lerp(transform.rotation, ship.GetComponent<SpaceShipMovement>().playerInstance.transform.rotation, Time.deltaTime * 10);
        }

        Vector3 newPos = new Vector3(ship.GetComponent<SpaceShipMovement>().playerInstance.transform.position.x, ship.GetComponent<SpaceShipMovement>().playerInstance.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;
    }

    void FollowShip()
    {
        Vector3 newPos = new Vector3(ship.transform.position.x, ship.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;
    }


    public void SetFollowPlayer(bool val)
    {
        followPlayer = val;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxVelocity = 3;
    public float rotationSpeed = 3;
    GameObject engine;

	public Vector3 gravityPull;
    public GameObject playerPrefab;
    

    Coroutine landingCoroutine;  
    public LayerMask PlanetLayer;

	public bool piloted = false;
	public bool landed = false;
	public LaunchPad launchPad;
    

	private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        engine = GameObject.FindGameObjectWithTag("Engine");
        engine.SetActive(true);

    }


	public void EnterShip(){

		if (landed) {
			TakeOff ();
		}
		GetComponent<SpaceShoot> ().enabled = true;
		piloted = true;
	}

	public void TakeOff(){
		rb.bodyType = RigidbodyType2D.Dynamic;
		landed = false;	
		launchPad.ExitPlanet ();
	}


    private void Update()
	{

		if (piloted) {
		
			if (Input.GetKeyDown ("f")) {
				GameObject player = Instantiate (playerPrefab, transform.position + transform.right, transform.rotation) as GameObject;
				player.GetComponent<Rigidbody2D> ().AddForce (rb.velocity.normalized * (rb.velocity.magnitude + 100)); 
				GetComponent<SpaceShoot> ().enabled = false;
				piloted = false;

				if (launchPad != null) {
					launchPad.EnterPlanet ();
				}
			}

			transform.Rotate (0, 0, Input.GetAxis ("Horizontal") * -1 * Time.deltaTime * 100);

			rb.AddForce ((transform.up * Input.GetAxis ("Vertical") * Time.deltaTime * 250) + gravityPull);
		} else {
			rb.drag = 0.5f;
			rb.AddForce (gravityPull);
		}

			if (rb.velocity.magnitude > maxVelocity) {
				rb.velocity = rb.velocity.normalized * maxVelocity;
			}
	}


}
	
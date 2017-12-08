using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public GameObject launchPad;

    public GameObject Canvas;

    public float fuel = 100;
    public Slider fuelSlider;
    

	private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        engine = GameObject.FindGameObjectWithTag("Engine");
        engine.SetActive(false);
        Canvas.SetActive(false);

    }


	public void EnterShip(){

        if (engine == null)
        {
            engine = GameObject.FindGameObjectWithTag("Engine");
            engine.SetActive(false);
        }

        Camera.main.GetComponent<CameraFollowSpaceShip> ().followPlayer = false;
		GameObject.Find ("CheckpointManager").GetComponent<CheckpointManager> ().playerCheckpoint = gameObject.GetComponent<Checkpoint> ();

		if (landed) {
			TakeOff ();
		}
		GetComponent<SpaceShoot> ().enabled = true;
        Canvas.SetActive(true);

		//piloted = true;
		StartCoroutine(pilotDelay());
	}

	IEnumerator pilotDelay(){
		yield return new WaitForEndOfFrame ();
        yield return new WaitForEndOfFrame();
        piloted = true;
	}

	public void TakeOff(){
		rb.bodyType = RigidbodyType2D.Dynamic;
		landed = false;	
		launchPad.GetComponent<LaunchPad>().ExitPlanet ();
	}


    private void Update()
	{

		if (piloted) {

            if (Input.GetKeyDown("r"))
            {
                GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().RespawnShip();
            }

            if (Input.GetKeyDown ("f")) {
				print ("getout");
				GameObject player = Instantiate (playerPrefab, transform.position + transform.right, transform.rotation) as GameObject;

				player.GetComponent<Rigidbody2D> ().AddForce (rb.velocity.normalized * (rb.velocity.magnitude + 100)); 
				player.GetComponent<controller> ().ship = gameObject;
				GetComponent<SpaceShoot> ().enabled = false;
                Canvas.SetActive(false);
                piloted = false;

				if (launchPad != null) {
					launchPad.GetComponent<LaunchPad>().EnterPlanet ();
				}
			}

			transform.Rotate (0, 0, Input.GetAxis ("Horizontal") * -1 * Time.deltaTime * 100);

			if (Input.GetAxis ("Vertical") > 0 && fuel > 0) {
				//engine particles 
				engine.SetActive (true);
                fuel -= 2 * Time.deltaTime;

                fuelSlider.value = fuel;
                rb.AddForce((transform.up * Input.GetAxis("Vertical") * Time.deltaTime * 250) + gravityPull);
                //fuel 
            } else {
				engine.SetActive (false);
                //fuel = 0;
                fuelSlider.value = fuel;
                rb.AddForce(gravityPull);
            }

			
		} else {
			rb.drag = 0.5f;
			rb.AddForce (gravityPull);
		}

			if (rb.velocity.magnitude > maxVelocity) {
				rb.velocity = rb.velocity.normalized * maxVelocity;
			}
	}


}
	
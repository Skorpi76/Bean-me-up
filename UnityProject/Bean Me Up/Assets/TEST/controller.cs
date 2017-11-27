using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {

	// Use this for initialization
	public LayerMask walkLayer;
	public float maxWalkSpeed;
	bool falling;
	public bool inSpace = true;
	Vector3 lastInputVelocity;
	float controlFactor = 1;
	Rigidbody2D rb;
	public Vector3 gravityPull;

	public LayerMask shipLayerMask;

	public int jetPackCharge = 3;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	IEnumerator JetPackReCharge(){
		yield return new WaitForSeconds (3);
		jetPackCharge++;
	}

	// Update is called once per frame
	void Update () {

		if (inSpace) {
		//space movement

			//rotation
			transform.Rotate (0, 0, Input.GetAxis ("Horizontal") * -1 * Time.deltaTime * 60);

			if (Input.GetKeyDown ("space") && jetPackCharge > 0) {
				rb.AddForce (transform.up * 100);
				jetPackCharge--;
				StartCoroutine(JetPackReCharge());
			}

			if (Input.GetKey (KeyCode.LeftShift)) {

				rb.drag = 0.6f;

			} else {
				rb.drag = 0;
			}


		} else {
		//planet movement

			//jump
			if (Input.GetKeyDown ("space") && !falling) {
				rb.AddForce (transform.up * 150);
			}

			//gravity
			rb.AddForce (gravityPull);

			// store player velocity in local space
			Vector3 localSpaceVelocity = transform.InverseTransformDirection (rb.velocity);

			//raycast to deturmin if we are on the ground
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up * -1, 1.2f, walkLayer);
			if (hit.collider != null) {
				falling = false;
				controlFactor = 1f;
				localSpaceVelocity = new Vector3 (Input.GetAxis ("Horizontal") * Time.deltaTime * 200 * controlFactor, localSpaceVelocity.y, 0);
				//rb.MovePosition( (transform.position + (transform.right * Input.GetAxis ("Horizontal") * Time.deltaTime * 4)));

			} else {
				falling = true;
				controlFactor = 0.8f;
				localSpaceVelocity = new Vector3 (localSpaceVelocity.x * 0.2f + Input.GetAxis ("Horizontal") * Time.deltaTime * 200 * controlFactor, localSpaceVelocity.y, 0);
			}

			//re-apply updated velocity
			rb.velocity = transform.TransformDirection (localSpaceVelocity);
		}

		if(Input.GetKeyDown("f")){
		//get in ship

			RaycastHit2D hit = Physics2D.CircleCast (transform.position, 2, transform.forward, 10, shipLayerMask);
			if (hit.collider != null) {
				if (hit.collider.tag == "Ship") {

					hit.collider.GetComponent<SpaceShipMovement1> ().EnterShip ();
					Camera.main.GetComponent<CameraFollowSpaceShip> ().followPlayer = false;
					Destroy (gameObject);
				
				}
			}

		}

	}
}

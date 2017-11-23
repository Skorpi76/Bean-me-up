using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanBoyMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float walkSpeed = 5;
    //bool jumping = false;
    public GameObject spriteObject;

    public LayerMask playerWalk;
    public LayerMask shipLayerMask;

	public Transform groundRay; 
	public float maxJumpTime;
	Vector2 jump;
    [HideInInspector]
    public Vector3 planetCore;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		jump = new Vector2 (0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            print("f pressed");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1, transform.forward, 10, shipLayerMask);
            if (hit.collider.tag == "Player")
            {
                
                hit.collider.GetComponent<SpaceShipMovement>().StartTakeOff();
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
	{

		if (Quaternion.FromToRotation (Vector3.down, (new Vector3 (planetCore.x, planetCore.y, 0) - new Vector3 (transform.position.x, transform.position.y, 0)).normalized) == Quaternion.FromToRotation (Vector3.down, Vector3.up)) {
			//print("Dont flip out bean boy");
		} else {
			transform.rotation = Quaternion.Lerp (Quaternion.FromToRotation (Vector3.down, (new Vector3 (planetCore.x, planetCore.y, 0) - new Vector3 (transform.position.x, transform.position.y, 0)).normalized), transform.rotation, Time.fixedDeltaTime);
		}

		//gravity
		Vector2 gravity = new Vector2 (0, 0);
		RaycastHit2D gravityHit = Physics2D.Raycast (groundRay.position, transform.up * -1, 0.05f, playerWalk);
		if (gravityHit.collider == null) {
			gravity = transform.up * -10;
		}

		//horizontal movement
		Vector2 vInput = transform.right * Input.GetAxis ("Horizontal") * Time.deltaTime * 200;

		//jump
		if (Input.GetKeyDown ("space")) {
			RaycastHit2D jumpHit = Physics2D.Raycast (groundRay.position, transform.up * -1, 0.1f, playerWalk);
			if (jumpHit.collider != null) {
				print ("try jumping");
				StartCoroutine (Jump ());
			}
		}

		//add up all velocitys
		rb.velocity = vInput + gravity + jump; 

        
	}

    IEnumerator Jump()
    {
		print ("jump co");
		float startJumpTime = Time.time;
		while (Input.GetKey("space") &&  Time.time - startJumpTime > maxJumpTime)
        {
			jump = transform.up * 100;
			print ("jumping");
            yield return null;
        }
		//jump = new Vector2 (0, 0);
    }
}

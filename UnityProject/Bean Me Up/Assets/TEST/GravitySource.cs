using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySource : MonoBehaviour {

    public float range;
	float surfaceRadius;
	public float gravityForce = 5000;
	public LayerMask GravityObjects;
	// Use this for initialization
	void Start () {
		surfaceRadius = GetComponent<Collider2D> ().bounds.size.x / 2;

		if(range == 0)
		range = surfaceRadius * 3;
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, range, GravityObjects);

        foreach (Collider2D col in objects) {
			if (col.attachedRigidbody != null ){
			

				Vector2 Direction = (transform.position - col.transform.position).normalized;
				float Distance = Vector3.Distance (col.transform.position, transform.position) - surfaceRadius;
				float factor = Mathf.Clamp( ((range - surfaceRadius) - Distance) / (range - surfaceRadius),0,1);
				Vector3 gForce =  Direction * (gravityForce*factor) * col.attachedRigidbody.mass * Time.deltaTime;
				if (col.tag == "Ship") {
					col.GetComponent<SpaceShipMovement1> ().gravityPull = gForce;
				} else if (col.tag == "Player") {
					
					col.GetComponent<controller> ().inSpace = false;
					col.GetComponent<controller> ().gravityPull = gForce;

					//rotation
					//print (Distance / (range - surfaceRadius - 0.5f));
					col.transform.rotation = Quaternion.Lerp( Quaternion.AngleAxis (Mathf.Atan2 (Direction.y, Direction.x) * Mathf.Rad2Deg + 90, Vector3.forward), col.transform.rotation , Distance / (range - surfaceRadius) );
					//col.transform.rotation = Quaternion.AngleAxis (Mathf.Atan2 (Direction.y, Direction.x) * Mathf.Rad2Deg + 90, Vector3.forward);
					Camera.main.GetComponent<CameraFollowSpaceShip> ().followPlayer = true;
					Camera.main.GetComponent<CameraFollowSpaceShip> ().player = col.gameObject;
				}else{

					col.attachedRigidbody.AddForce (Direction * (gravityForce/factor) * col.attachedRigidbody.mass * Time.deltaTime);

				}
                //print(Mathf.Atan2(Direction.x, Direction.y));

			}
        }
	}
}

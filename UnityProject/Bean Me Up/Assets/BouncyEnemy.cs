using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyEnemy : MonoBehaviour {
    public float waitAmount = 2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void BounceAgain()
    {
        StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        //rb.AddForce(transform.right * dir.x * bounceAmount);
        yield return new WaitForSeconds(waitAmount);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * dir.x, 0.7f, inchLayerMask);
        //if (hit.collider != null)
        {
        //    print("hit a wall");
        //    dir.x = dir.x * -1;
        }
        //InchAgain();
    }
}

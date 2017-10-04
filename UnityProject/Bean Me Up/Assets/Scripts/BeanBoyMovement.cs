﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanBoyMovement : MonoBehaviour {

    Rigidbody2D rb;
    public float walkSpeed = 5;
    bool jumping = false;
    public GameObject spriteObject;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 newPosition = rb.transform.position + (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * walkSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Input.GetKeyDown("space") && !jumping) {
            StartCoroutine("Jump");
        }
	}

    IEnumerator Jump() {
        float t = 0;
        jumping = true;
        while (t < .8f) {
            t += 3 * Time.deltaTime;
            spriteObject.transform.position = rb.transform.position + new Vector3(0,t,0);
            yield return null;
        }
        while (t > 0)
        {
            t -= 3 * Time.deltaTime;
            spriteObject.transform.position = rb.transform.position + new Vector3(0, t, 0);
            yield return null;
        }
        jumping = false;
    }
}
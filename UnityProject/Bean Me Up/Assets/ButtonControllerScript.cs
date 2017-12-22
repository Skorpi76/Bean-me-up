using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControllerScript : MonoBehaviour {

    public Animator animator;
    public GameObject[] objectsToActivate;

	// Use this for initialization
	void Start () {
		
	}

    void Pressed() {
		foreach (GameObject g in objectsToActivate) {
			g.SendMessage ("ActivateObject");
		
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Player") {
             Pressed();
            Debug.Log("pressed button");
            animator.SetTrigger("Pressed");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            animator.SetTrigger("Depressed");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

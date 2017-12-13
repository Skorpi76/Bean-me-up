using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

    void Start()
    {
       
    }

    //Pick up gun
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    
}

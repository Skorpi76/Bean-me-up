using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyObjectScript : MonoBehaviour
{

    Vector3 resetPosition;
	Quaternion resetRotation;
    // Use this for initialization
    void Start()
    {
        resetPosition = gameObject.transform.position;
        resetRotation = gameObject.transform.rotation;
    }

    public void ActivateObject()
    {
        gameObject.transform.position = resetPosition;
        gameObject.transform.rotation = resetRotation;
    }

}

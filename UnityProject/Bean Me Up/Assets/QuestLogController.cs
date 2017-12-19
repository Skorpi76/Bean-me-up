using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogController : MonoBehaviour {

    public Canvas questCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Tab))
        {
            questCanvas.enabled = true;
        }
        else {
            questCanvas.enabled = false;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetLoader : MonoBehaviour {


    public Object sceneToLoad;
    public GameObject planet;


	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<SpaceShipMovement>().CanLand = true;
            collision.GetComponent<SpaceShipMovement>().PlanetCore = this.transform.position;
            print("load Scene " + sceneToLoad.name);
            SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Additive);

            //SceneManager.GetSceneByName(sceneToLoad.name).



        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<SpaceShipMovement>().CanLand = false;
            print("Unload Scene " + sceneToLoad.name);
            SceneManager.UnloadSceneAsync(sceneToLoad.name);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetLoader : MonoBehaviour {

	Color colorStart;
    public UnityEngine.Object sceneToLoad;
    //public GameObject planet;
    public GameObject AtmosphereObj;
    bool OnPLanet = false;
    public float planetRadius;
	// Use this for initialization
	void Start () {
		colorStart = AtmosphereObj.GetComponent<Renderer> ().material.color;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<SpaceShipMovement>().CanLand = true;
            collision.GetComponent<SpaceShipMovement>().PlanetCore = this.transform.position;
            print("load Scene " + sceneToLoad.name);
            SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Additive);
            OnPLanet = true;
			Fade ();
            //SceneManager.GetSceneByName(sceneToLoad.name).

        }
    }

    public void Fade() {
        StartCoroutine(FadeOut(GameObject.FindGameObjectWithTag("Player").gameObject));
    }

    IEnumerator FadeOut(GameObject player) {
        //Color colorStart = AtmosphereObj.GetComponent<Renderer>().material.color;
        Color colorEnd = colorStart;
        colorEnd.a = 0;
        while (player.GetComponent<SpaceShipMovement>().playerState == PlayerState.Ship && OnPLanet)
        {
            print((Vector3.Distance(transform.position, player.transform.position) - planetRadius) / (GetComponent<CircleCollider2D>().radius - planetRadius));
            AtmosphereObj.GetComponent<Renderer>().material.color = Color.Lerp( colorEnd, colorStart,(Vector3.Distance(transform.position, player.transform.position) - planetRadius) / (GetComponent<CircleCollider2D>().radius - planetRadius));
            yield return null;
        }
        AtmosphereObj.GetComponent<Renderer>().material.color = new Color(255, 255, 255,0);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<SpaceShipMovement>().CanLand = false;
            print("Unload Scene " + sceneToLoad.name);
            SceneManager.UnloadSceneAsync(sceneToLoad.name);
            OnPLanet = false;
			AtmosphereObj.GetComponent<Renderer>().material.color = colorStart;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

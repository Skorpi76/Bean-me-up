using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogController : MonoBehaviour {

    public Canvas questCanvas;
	public int PlanetCount = 1;
	public int EnemyCount = 0;
	public int BoosterCount = 0;

	public GameObject planetText;
	public GameObject enemyText;
	public GameObject boosterText;

	public GameObject NotificationPrefab;
	GameObject notificationReference;

	public bool[] boosterCollected = new bool[]{
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false,
		false
	};

	// Use this for initialization
	void Start () {
		
	}


	public void NewPlanet(){
		PlanetCount++;
		if (PlanetCount <= 5) {
			planetText.GetComponent<TextMeshProUGUI>().text = "- Visit all the planets\n\t " + PlanetCount + " of 5";
			CreateNotification (PlanetCount + " out of 5 planets visited.");
		}
	}

	public void AddEnemy(){
		EnemyCount++;
		if (EnemyCount < 10) {
			enemyText.GetComponent<TextMeshProUGUI>().text = "- Defeat 10 enemies\n\t " + EnemyCount + " of 10";
			CreateNotification (EnemyCount + " out of 10 enemies killed.");
		}
	}

	public void AddBeanBooster(int ID){
		BoosterCount++;
		boosterCollected [ID] = true;
		if (BoosterCount < 15) {
			boosterText.GetComponent<TextMeshProUGUI>().text = "- Collect all of the bean boosters\n\t " + BoosterCount + " of 15";
			CreateNotification (BoosterCount + " out of 15 boosters collected.");
		}
	}

	public void CreateNotification(string notificationText){
		if (notificationReference != null) {
			Destroy (notificationReference);
		}
		notificationReference = Instantiate (NotificationPrefab) as GameObject;
		notificationReference.transform.Find("Canvas").Find("Panel").Find("TextMeshPro Text").GetComponent<TextMeshProUGUI> ().text = notificationText;
		Debug.Log (notificationText);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour {
    public int maxHealth;
    public int health;


	bool hasWorldHealthBar = false;
	Transform canvas;
    // Use this for initialization
    void Start () {
        health = maxHealth;	
		if (gameObject.transform.Find ("Canvas") && gameObject.tag != "Player") {
			Debug.Log ("Has world health bar");
			hasWorldHealthBar = true;
			canvas = gameObject.transform.Find ("Canvas");
			canvas.transform.Find("Health").gameObject.GetComponent<Slider> ().maxValue = maxHealth;
			canvas.transform.Find("Health").gameObject.GetComponent<Slider> ().value = health;
		}
	}


	void Update(){
		if (hasWorldHealthBar) {
			canvas.rotation = Quaternion.identity;
		}
	}

    public void ResetHealth() {
        health = maxHealth;
        if (gameObject.tag == "Ship")
        {
            transform.Find("Canvas").Find("Health").gameObject.GetComponent<Slider>().value = health;
        }
    }

    public virtual void ModifyHealth(int amount)
    {
       
        
		if (gameObject.tag == "Ship") {
			if (gameObject.GetComponent<SpaceShipMovement1> ().piloted) {
				health += amount;
				transform.Find ("Canvas").Find ("Health").gameObject.GetComponent<Slider> ().value = health;
			}
		}
		else if(gameObject.tag == "Player"){
			health += amount;
			transform.Find ("HealthCanvas").Find ("Health").gameObject.GetComponent<Slider> ().value = health;
		}else if(hasWorldHealthBar){
			health += amount;
			canvas.transform.Find("Health").gameObject.GetComponent<Slider> ().value = health;
		}
		else{
			health += amount;
		}
        if (health <= 0)
        {
            health = 0;
            OnDeath();
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public virtual void OnDeath()
    {
        if (gameObject.tag == "Ship")
        {
            GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().RespawnShip();
        }
        else if (gameObject.tag == "Player") {
            GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().RespawnPlayer();
        }
        else
        {
			Camera.main.GetComponent<QuestLogController> ().AddEnemy ();
            Destroy(this.gameObject);
        }
    }

}

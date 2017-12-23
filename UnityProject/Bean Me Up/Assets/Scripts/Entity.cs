﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour {
    public int maxHealth;
    public int health;

    // Use this for initialization
    void Start () {
        health = maxHealth;	
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
		}else {
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

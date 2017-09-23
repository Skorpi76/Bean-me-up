using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour {
    public int maxHealth;
    public int health;

    // Use this for initialization
    void Start () {
        health = maxHealth;	
	}

    public virtual void ModifyHealth(int amount)
    {
       
        health += amount;
        Debug.Log(health);
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
        Destroy(this.gameObject);      
    }

}

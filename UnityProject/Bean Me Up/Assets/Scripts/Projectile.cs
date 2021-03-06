﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed = 10;
    public int damage = -10;
	public bool isEnemyProjectile = false;

    private void Start()
    {
        Destroy(gameObject, 6);
    }

    public void Launch()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.up * speed;
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        OnCollision(other.gameObject);
      
        Destroy(this.gameObject);
    }
    protected virtual void OnCollision(GameObject obj)
    {
		if (isEnemyProjectile) {
			if (obj.tag == "Player" || obj.tag == "Ship") {
				Entity entity = obj.GetComponent<Entity> ();
				entity.ModifyHealth (damage);
			} else {
				//Debug.Log ("Enemy to enemy attack");
			}
		} else {
			Entity entity = obj.GetComponent<Entity>();
			if (entity != null)
			{

				entity.ModifyHealth(damage);

			}
		}
        
    }
}

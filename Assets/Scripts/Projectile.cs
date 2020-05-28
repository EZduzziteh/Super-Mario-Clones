using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

 
    public float speed;

    public float lifetime;

	// Use this for initialization
	void Start () {

        
        if (lifetime <= 0)
        {
           
            lifetime = 2.0f;

      
        }

        
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        
        Destroy(gameObject, lifetime);
    }

    
    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag!="ground")
        Destroy(gameObject);
    }
}

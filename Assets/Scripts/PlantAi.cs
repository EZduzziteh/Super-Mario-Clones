using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAi : MonoBehaviour {
    public int scorevalue;
    public GameManager manager;
    public float speed;
    public float initialy;
    public float buffer;
    // Use this for initialization
    void Start () {
        initialy = transform.position.y;
        scorevalue = 200;
        manager = FindObjectOfType<GameManager>();
        speed = 0.001f;
        buffer = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0f, speed));
        if ( transform.position.y>initialy + buffer )
        {
            speed = -speed;
        }else if (transform.position.y < initialy - buffer)
        {
            speed = -speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            TriggerDeath();
        }
        else if (collision.gameObject.tag == "StarPlayer")
        {
            TriggerDeath();

        }
    }
    public void TriggerDeath()
    {
        manager.score += scorevalue;
        Destroy(gameObject);
    }
}

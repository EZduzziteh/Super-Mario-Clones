using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    public Character player;
    Rigidbody2D rb;
    bool islethal;
    AudioSource aud;
    float starttime;
    // Use this for initialization
    void Start () {
        aud = GetComponent<AudioSource>();
        player = FindObjectOfType<Character>();
        islethal = false;
        rb = GetComponent<Rigidbody2D>();
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.y = -scaleFactor.y;
        transform.localScale = scaleFactor;
        rb.velocity = new Vector2(0, 0);
        starttime = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > player.transform.position.x + 2)
        {
            Destroy(gameObject);
        } else if (transform.position.x < player.transform.position.x - 2)
        {
            Destroy(gameObject);
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EdgeCollider")
        {
            if (collision.transform.position.x > transform.position.x)
            {
                //Shell Moves left

                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(-50, 0));
            }
            else if (collision.transform.position.x < transform.position.x)
            {
                //shell moves right
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(50, 0));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            Destroy(gameObject);
        }
        if (!islethal)
        {
            if ((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "StarPlayer"))
            {
                if (starttime + 0.5 > Time.deltaTime)
                {
                    islethal = true;

                    aud.Play();
                    if (player.transform.position.x > transform.position.x)
                    {
                        //Shell Moves left
                        rb.velocity = new Vector3(0, 0, 0);
                        rb.AddForce(new Vector2(-50, 0));
                    }
                    else if (player.transform.position.x < transform.position.x)
                    {
                        //shell moves right
                        rb.velocity = new Vector3(0, 0, 0);
                        rb.AddForce(new Vector2(50, 0));
                    }
                }
            }
            else
            {
                aud.Play();
                if (collision.transform.position.x > transform.position.x)
                {
                    //Shell Moves left
                 
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(new Vector2(-50, 0));
                    
                }
                else if (collision.transform.position.x < transform.position.x)
                {
                    
                    
                    
                        //shell moves right
                        rb.velocity = new Vector3(0, 0, 0);
                        rb.AddForce(new Vector2(50, 0));
                    
                }
            }
        }
        else if (islethal)
        {


            if (collision.gameObject.tag == "Player")
            {
                if (player.transform.position.y <= transform.position.y)
                {
                    if (player.isFirePower)
                    {
                        player.isFirePower = false;
                        player.anim.SetBool("isFire", false);
                    }
                    else
                    {
                        player.TriggerDeath();
                    }
                }
            }
            else
            {
                aud.Play();
                if (collision.transform.position.x > transform.position.x)
                {
                    //Shell Moves left
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(new Vector2(-50, 0));
                }
                else if (collision.transform.position.x < transform.position.x)
                {
                    //shell moves right
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(new Vector2(50, 0));
                }
            }
        }
    }
    }


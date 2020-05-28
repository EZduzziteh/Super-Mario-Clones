using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    
    public string type;
    bool iscollected;
    public AudioSource asource;
    public Renderer rend;
    public MusicManager mus;
    public int scorevalue;
    public GameManager manager;
    public int coinvalue;
    public float speed;
    public Rigidbody2D rb;
    public bool isFacingLeft;
   public  bool canmove;
    public bool collectonload;
	// Use this for initialization
	void Start () {
        
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
        mus = FindObjectOfType<MusicManager>();
        iscollected = false;
        asource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        if(collectonload)
        {
            if (coinvalue > 0)
            {
                manager.coins += coinvalue;
                manager.score += scorevalue;
                asource.Play();
                iscollected = true;
            }
        }
        if (tag == "Star")
        {
            rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        }

    }
	
	// Update is called once per frame
	void Update () {
        
        
        if (canmove)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (iscollected)
        {
            if (!asource.isPlaying)
            {
                
                Destroy(gameObject);
            }
        }
        
        
            
        
	}

    void flipx()
    {

        isFacingLeft = !isFacingLeft;


        Vector3 scaleFactor = transform.localScale;


        scaleFactor.x = -scaleFactor.x;


        transform.localScale = scaleFactor;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            if (!iscollected)
            {
                manager.coins += coinvalue;
                manager.score += scorevalue;
                if (tag == "Star")
                {
                    col.tag = "StarPlayer";
                    mus.PlayInvincible();
                }else if (tag == "1up")
                {
                    manager.lives++;
                }
                rend.enabled = false;
                asource.Play();
                iscollected = true;
                
            }
            

        }else if (col.gameObject.tag == "StarPlayer")
        {

            if (!iscollected)
            {
                manager.coins += coinvalue;
                manager.score += scorevalue;
                if (tag == "Star")
                {
                    col.tag = "StarPlayer";
                    mus.PlayInvincible();
                }
                else if (tag == "1up")
                {
                    manager.lives++;
                }
                rend.enabled = false;
                asource.Play();
                iscollected = true;

            }


        }else if (col.gameObject.tag == "EdgeCollider")
        {
            speed = -speed;
            flipx();
        }
    }
}

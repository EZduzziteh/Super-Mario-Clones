using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_AI : MonoBehaviour
{
    public float startspeed;
    private float speed;
    public Rigidbody2D rb;
    Animator anim;
    public BoxCollider2D box;
    bool isFacingLeft;
    AudioSource aud;
    public int scorevalue;
    public GameManager manager;
    // Use this for initialization
    void Start()
    {
        scorevalue = 100;
        manager = FindObjectOfType<GameManager>();
        aud = GetComponent<AudioSource>();
        speed = 0;
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            speed = startspeed;
        }else if (collision.gameObject.tag == "EdgeCollider")
        {
            speed = -speed;
            flipx();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EdgeCollider")
        {
            speed = -speed;
            flipx();
        }
        else if (collision.gameObject.tag == "Projectile")
        {
            TriggerDeath();

        } else if (collision.gameObject.tag == "Player" && collision.transform.position.y > transform.position.y)
        {
            TriggerDeath();
        }
        else if (collision.gameObject.tag == "StarPlayer" )
        {
            TriggerDeath();
        }
        else if (collision.gameObject.tag == "Shell")
        {
            TriggerDeath();
        }
        else if (collision.gameObject.tag == "Respawn")
        {
            Destroy(gameObject);
        }
    }
    void TriggerDeath()
    {
        anim.SetBool("isDead", true);
        aud.Play();
        speed = 0;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        flipy();
        box.enabled = false;
        manager.score += scorevalue;
    }
    void flipx()
    {

        isFacingLeft = !isFacingLeft;


        Vector3 scaleFactor = transform.localScale;


        scaleFactor.x = -scaleFactor.x;


        transform.localScale = scaleFactor;
    }
    void flipy()
    {

        

        Vector3 scaleFactor = transform.localScale;


        scaleFactor.y = -scaleFactor.y;


        transform.localScale = scaleFactor;
    }
}

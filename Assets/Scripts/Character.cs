using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {
    public bool isFirePower;

    public MusicManager mus;
    public AudioClip projectile;
    public AudioClip jump;
    public AudioClip kick;
    public AudioClip powerdown;
    public AudioClip flagpole;
    public GameManager manager;
    public bool isDead;
    public float cooldown;
    AudioSource aud;

    public bool isWon;
   
    public Rigidbody2D rb;

    public float speed;

    public float jumpForce;

    public bool isstar;

    public bool isGrounded;

    public LayerMask isGroundLayer;

    public Transform groundCheck;
    
    public float groundCheckRadius;

    public Animator anim;

    public Transform projectileSpawnPoint;
    
    public Projectile projectilePrefab;

    public AudioClip Flagpole;
    public float projectileSpeed;
    
    public bool isFacingLeft;
    float deathtimer;
    float counter = 999999;
    bool isflagpole = false;
	// Use this for initialization
	void Start () {
        cooldown = 0;
        mus = FindObjectOfType<MusicManager>();
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
        aud = GetComponent<AudioSource>();

       
        if (speed <= 0)
        {
         
            speed = 5.0f;

          
            Debug.LogWarning("Speed not set on " + name + ". Defaulting to " + speed);
        }

       
        if (jumpForce <= 0)
        {
           
            jumpForce = 5.0f;

          
            Debug.LogWarning("JumpForce not set on " + name + ". Defaulting to " + jumpForce);
        }

     
        if (!groundCheck) 
        {
           
            Debug.LogError("GroundCheck not found on " + name);
        }

      
        if (groundCheckRadius <= 0)
        {
         
            groundCheckRadius = 0.2f;

         
            Debug.LogWarning("GroundCheckRadius not set on " + name + ". Defaulting to " + groundCheckRadius);
        }

        
        anim = GetComponent<Animator>();
        
       
        if (!anim) 
        {
           
            Debug.LogError("Animator not found on " + name);
        }

        // Check if variable is set to something
        if (!projectileSpawnPoint)
        {
            
            Debug.LogError("ProjectileSpawnPoint not found on " + name);
        }

      
        if (!projectilePrefab)
        {
           
            Debug.LogError("ProjectilePrefab not found on " + name);
        }

     
        if (projectileSpeed <= 0)
        {
           
            projectileSpeed = 7.0f;
            
            Debug.LogWarning("ProjectileSpeed not set on " + name + ". Defaulting to " + projectileSpeed);
        }
    }
	
	// Update is called once per frame
	void Update () {
        

        if (isDead)
        {
            if (Time.time > deathtimer)
            {
                manager.ReloadLevel();
            }
        }
        


        float moveValue = Input.GetAxisRaw("Horizontal");
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 
            groundCheckRadius, isGroundLayer);

        if (isGrounded)
        {
            
            anim.SetBool("isJumping",false);
        }

        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
          
           // Debug.Log("Jump");
            anim.SetBool("isJumping", true);
           
            rb.velocity = Vector2.zero;
            aud.clip = jump;
            
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            aud.Play();

        }

       
        if (Input.GetButtonDown("Fire1"))
       
        {
            aud.clip = projectile;
            if (isDead)
            {
                manager.ReloadLevel();
            }
            if (isWon)
            {
                aud.clip = flagpole;
                aud.Play();
                if (!isflagpole)
                {
                    manager.score += Mathf.RoundToInt(manager.timer)*10;
                    if (transform.position.y >= 0.708)
                    {
                        manager.score += 5000;

                    }else if (transform.position.y >= 0.495)
                    {
                        manager.score += 4000;

                    }else if (transform.position.y >= 0.27)
                    {
                        manager.score += 3000;

                    }else if (transform.position.y >= 0.057)
                    {
                        manager.score += 2000;
                    }
                    else if (transform.position.y >= -0.1)
                    {
                        manager.score += 1000;

                    }
                    else if (transform.position.y >= -0.37)
                    {
                        manager.score += 500;
                    }
                    else if (transform.position.y >= -0.598)
                    {
                        manager.score += 100;

                    }
                    
                    

                   
                }
                isflagpole = true;
                
                counter = Time.time + 3;
                
            }


            if (isFirePower)
            {

                if (cooldown < Time.time)
                {
                    fire();
                }
                
            }
        }
        if (isflagpole)
        {

            transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), new Vector2(14.99f, -0.63f), Time.fixedDeltaTime);
        }
        
        
            if (counter < Time.time)
            {
               manager.GoToWin();
            }
        
      
        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

      
        anim.SetFloat("speed", Mathf.Abs(moveValue));

     

       
        if ((moveValue > 0 && isFacingLeft) || (moveValue < 0 && !isFacingLeft))
           
            flip();

    }

   
    void fire()
    {
        aud.Play();
        Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, 
            projectileSpawnPoint.rotation);

        
            temp.speed = projectileSpeed;
        if (isFacingLeft)
        {
            temp.speed = -projectileSpeed;
        }

        cooldown = Time.time + 1;
        
    }

  
    void OnTriggerEnter2D(Collider2D c)
    {
    
        //Debug.Log(c.gameObject.tag);

        
        if(c.gameObject.tag == "FireFlower")
        {
            isFirePower = true;
            anim.SetBool("isFire", true);
            
        }else if (c.gameObject.tag == "Finish")
        {
            rb.simulated = false;
            anim.SetBool("isFinished", true);
            isWon = true;
         
        }else if (c.gameObject.tag == "Respawn")
        {
            TriggerDeath();
        }else if (c.gameObject.tag == "Star")
        {
            
            isstar = true;
        }
    }

   
    void flip()
    {
        if (!isWon)
        {
            if (!isDead)
            {
                isFacingLeft = !isFacingLeft;


                Vector3 scaleFactor = transform.localScale;


                scaleFactor.x = -scaleFactor.x;


                transform.localScale = scaleFactor;
            }
        }
    }
    public void TriggerDeath()
    {
        anim.SetBool("isDead", true);
        isDead = true;
        rb.simulated = false;
        mus.PlayDeathSound();
        deathtimer = Time.time + 3.2f;
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy"){
            if (!isstar)
            {
                if (transform.position.y > collision.transform.position.y)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                else
                {
                    if (isFirePower)
                    {
                        isFirePower = false;
                        anim.SetBool("isFire", false);
                        aud.clip = powerdown;
                        aud.Play();
                    }
                    else
                    {
                        TriggerDeath();
                    }
                }
            }
            
        }else if (collision.gameObject.tag == "Plant")
        {
            if (!isstar)
            {
                if (isFirePower)
                {
                    isFirePower = false;
                    anim.SetBool("isFire", false);
                    aud.clip = powerdown;
                    aud.Play();
                }
                else
                {
                    TriggerDeath();
                }
            }
        }else if (collision.gameObject.tag == "Shell")
        {
            if (transform.position.y > collision.transform.position.y)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                aud.clip = kick;
                aud.Play();
            }
            
        }
    }
    
}

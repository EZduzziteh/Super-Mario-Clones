using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisiblock : MonoBehaviour
{

    public SpriteRenderer rend;
    
    public BoxCollider2D col;
    AudioSource aud;
    public GameManager manager;
    bool istriggered;
    public GameObject spawnpoint;
    public Collectible collectible;

    // Use this for initialization
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        col = GetComponent<BoxCollider2D>();
        istriggered = false;
        aud = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        rend.sortingOrder = -5;
        col.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Collided");
        if ((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "StarPlayer"))
        {
            if (collision.transform.position.y < transform.position.y)
            {
                {
                    if (!istriggered)
                    {
                        istriggered = true;
                        rend.sortingOrder = 4;
                        col.isTrigger = false;
                        Instantiate(collectible, spawnpoint.transform);
                        aud.Play();
                        manager.score += 0;
                        aud.Play();
                    }
                }
            }
        }
    }
}


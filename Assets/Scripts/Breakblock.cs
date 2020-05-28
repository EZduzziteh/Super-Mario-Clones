using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakblock : MonoBehaviour
{

    public SpriteRenderer rend;
    public GameObject spawnpoint;
    public BoxCollider2D col;
    AudioSource aud;
    public GameManager manager;
    bool istriggered;

    // Use this for initialization
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        col = GetComponent<BoxCollider2D>();
        istriggered = false;
        aud = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        rend.sortingOrder = -5;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
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
                        Destroy(col);
                        manager.score += 50;
                        aud.Play();
                    }
                }
            }
        }
    }
}


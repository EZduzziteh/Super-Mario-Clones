using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBlock : MonoBehaviour {

    public SpriteRenderer rend;
    public Collectible collectible;
    public GameObject spawnpoint;
    AudioSource aud;
    bool istriggered;
    public bool iscoin;
    
	// Use this for initialization
	void Start () {
        istriggered = false;
        aud = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        rend.sortingOrder = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Collided");
        if ((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "StarPlayer"))
        {
            if (!istriggered)
            {
                istriggered = true;
                rend.sortingOrder = 4;
                Debug.Log("Tried to instantiate");
                Instantiate(collectible, spawnpoint.transform);
                if (!iscoin)
                {
                    aud.Play();
                }
            }
        }
    }
}

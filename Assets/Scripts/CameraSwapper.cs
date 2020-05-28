using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapper : MonoBehaviour {

    public CameraController camcon;
    public Character player;
	// Use this for initialization
	void Start () {
        camcon = FindObjectOfType<CameraController>();
        player = FindObjectOfType<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((player.transform.position.x > transform.position.x - 1)&&(player.transform.position.x<transform.position.x+12))
        {
            if (player.transform.position.y > transform.position.y)
            {
                camcon.SwaptoSkyCam();
            }
            else
            {
                camcon.SwaptoMainCam();
            }
        }
	}

   
}

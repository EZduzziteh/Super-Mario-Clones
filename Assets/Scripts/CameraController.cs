using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject maincam;
    public GameObject skycam;
    public bool ismaincam;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwaptoMainCam()
    {
        ismaincam = true;
        skycam.SetActive(false);
        maincam.SetActive(true);
       
            
            
       
    }
    public void SwaptoSkyCam()
    {
        ismaincam = false;
        maincam.SetActive(false);
        skycam.SetActive(true);
    }
}

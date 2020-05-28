using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {


    public float leftconstraint= -14.78f;
    public float rightconstraint= 14.8f;
    public Transform player;
    public float camy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if ((player.position.x > leftconstraint)&&(player.position.x<rightconstraint) )
        {
            transform.position = new Vector3(player.position.x, camy, -10f);
        }
	}
}

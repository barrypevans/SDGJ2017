using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : Interactable {

    bool canTrip = false;
    public GameObject[] fallingObjects;

	// Use this for initialization
	void Start () {

	}
	
	void Update () {
        //UPDATE INPUT
        if (canTrip && Input.GetKeyDown(KeyCode.R))
            foreach (GameObject g in fallingObjects)
            {
                g.GetComponent<Rigidbody2D>().isKinematic = false;
                g.GetComponent<Rigidbody2D>().gravityScale = 20f;
            }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canTrip = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canTrip = false;
        }
    }

    protected override void Interact()
    {
       // throw new NotImplementedException();
    }
}

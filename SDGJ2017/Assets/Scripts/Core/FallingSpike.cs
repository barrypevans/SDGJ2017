using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : Interactable
{

    bool canTrip = false;
    public GameObject[] fallingObjects;

    private void Start()
    {
        if (GameManager.Instance._isSecondRun && GameManager.Instance.DidDropSpikes)
            foreach (GameObject g in fallingObjects)
            {
                g.GetComponent<Rigidbody2D>().isKinematic = false;
                g.GetComponent<Rigidbody2D>().gravityScale = 20f;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canTrip = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canTrip = false;
        }
    }

    protected override void Interact()
    {
        if (canTrip)
        {
            foreach (GameObject g in fallingObjects)
            {
                g.GetComponent<Rigidbody2D>().isKinematic = false;
                g.GetComponent<Rigidbody2D>().gravityScale = 20f;
            }
            GameManager.Instance.DidDropSpikes = true;
        }
    }
}

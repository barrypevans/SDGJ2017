using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
            GameManager.Instance.SetRespawnPosition(transform.position);
    }
}

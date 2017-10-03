using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public bool hasTriggered = false;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GameManager.Instance.SetRespawnPosition(transform.position);
            if (!hasTriggered)
            {
                //this will throw errors until you put a ding wav (sent by me) in resources/audio
                GameManager.Instance.GetComponent<MusicAudControl>().playClip("Ding");
                GameManager.Instance.GetComponent<MusicAudControl>().iterateTrack();
                hasTriggered = true;
            }
        }
    }
}

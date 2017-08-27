using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadVolume : MonoBehaviour {
    public string LevelName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        GameManager.Instance.LoadLevel(LevelName);
    }
}

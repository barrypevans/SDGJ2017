using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private GameObject _player;
    private Vector3 _respawnPosition;

    private void Start()
    {
        Instance = this;
        _player = GameObject.Find("player");
    }

    public void SetRespawnPosition(Vector3 pos){_respawnPosition = pos;}

    public void DoRespawn()
    {
        _player.transform.position = _respawnPosition;
    }


    private bool _hasGloves;



}

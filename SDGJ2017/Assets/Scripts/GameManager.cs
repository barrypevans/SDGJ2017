using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private GameObject _player;
    private GameObject _camera;
    private Vector3 _respawnPosition;

    private bool _isRespawning = false;


    private void Start()
    {
        Instance = this;
        _player = GameObject.Find("player");
        _camera = GameObject.Find("main-camera");
    }

    public void SetRespawnPosition(Vector3 pos){_respawnPosition = pos;}

    public void DoRespawn()
    {
        StartCoroutine(Co_DoRespawn());
    }

    public IEnumerator Co_DoRespawn()
    {
        if (_isRespawning)
            yield return null;
        _isRespawning = true;
        _player.transform.position = _respawnPosition;
        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _camera.GetComponent<CameraController>().SuspendMovment();
        _player.GetComponent<Locomotion>().SuspendMovment();
        yield return new WaitForSeconds(1f);
        _player.GetComponent<Locomotion>().ResumeMovment();
        _camera.GetComponent<CameraController>().ResumeMovment();
        _isRespawning = false;
    }

    private bool _hasGloves;



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
      
        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _camera.GetComponent<CameraController>().SuspendMovment();
        _player.GetComponent<Locomotion>().SuspendMovment();
        for (int i = 0; i < 4; i++)
        {
           
            _player.GetComponent<Renderer>().material.color = new Color(1,0,0,0);
            yield return new WaitForSeconds(.03f);
            _player.GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(.05f);
        }

        
        _player.transform.position = _respawnPosition;
        _player.GetComponent<Locomotion>().ResumeMovment();
        _camera.GetComponent<CameraController>().ResumeMovment();
        _isRespawning = false;
       
    }

    public void LoadLevel(string level)
    {
        StartCoroutine(Co_loadLevel(level));
      
    }

    private IEnumerator Co_loadLevel(string level)
    {
        _player.transform.position = _respawnPosition;
        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero; 
        _camera.GetComponent<CameraController>().SuspendMovment();
        _player.GetComponent<Locomotion>().SuspendMovment();
        //TODO: Add A Fade Here!
        yield return null;
        SceneManager.LoadScene(level);
    }


    public bool HasDash = true;// { get; private set; }
    public bool HasGloves  { get; private set; }


}

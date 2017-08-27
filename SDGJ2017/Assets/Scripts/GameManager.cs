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
    public bool _isSecondRun;

    public double _timer;
    public bool _timerRunning = false;

    private void OnEnable()
    {
        if (Instance != null)
            GameObject.Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {

        DontDestroyOnLoad(gameObject);
        _player = GameObject.Find("player");
        _camera = GameObject.Find("main-camera");
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0 && _isSecondRun) SetupSecondRun();
        _player = GameObject.Find("player");
        _camera = GameObject.Find("main-camera");

    }
    public void SetupSecondRun()
    {
        HasDash = false;
        HasGloves = false;
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


    public void EndGame()
    {
        if (!_isSecondRun)
        {
            _isSecondRun = true;
            //TODO: change this to be the right order
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Load the scoreboard here");
        }
    }

    public bool HasDash = false;    
    public bool HasGloves = false;

    public bool FirstRunHasDash = false;
    public bool FirstRunHasGloves = false;

    public void StartTimer()
    {
        _timerRunning = true;
    }

    public void PauseTimer()
    {
        _timerRunning = false;
    }

    private void Update()
    {
        if(_timerRunning)
            _timer += Time.deltaTime;
    }


}

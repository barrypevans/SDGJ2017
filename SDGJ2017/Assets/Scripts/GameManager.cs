using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private GameObject _player;
    private GameObject _camera;
    public Text _timerImage_Min_Sec;
    public Text _timerImage_Mili;

    private Vector3 _respawnPosition;
    private bool _isRespawning = false;
    public bool _isSecondRun;

    public double _timer;
    public bool _timerRunning = false;

    private void OnEnable()
    {
        if (Instance != null)
            GameObject.Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {

        DontDestroyOnLoad(gameObject);
        _player = GameObject.Find("player");
        _camera = GameObject.Find("main-camera");
        StartTimer();

        Debug.Log("seconds: " + PlayerPrefs.GetInt("seconds"));
        Debug.Log("minutes: " + PlayerPrefs.GetInt("minutes"));
        Debug.Log("miliseconds: " + PlayerPrefs.GetInt("miliseconds"));
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1 && _isSecondRun) SetupSecondRun();
        if (level == 0) GameObject.Destroy(gameObject);
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
            FirstRunHasDash = HasDash;
            FirstRunHasGloves = HasGloves;
            FirstRunHasShield = HasShield;
            _isSecondRun = true;
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayerPrefs.SetInt("seconds", (int)seconds);
            PlayerPrefs.SetInt("minutes", (int)minutes);
            PlayerPrefs.SetInt("miliseconds", (int)miliseconds);
            PlayerPrefs.SetInt("score-flag", 1);
            SceneManager.LoadScene(0);
        }
    }
    [HideInInspector]
    public bool HasDash = false;
    [HideInInspector]
    public bool HasGloves = false;
    [HideInInspector]
    public bool HasShield = false;
    [HideInInspector]
    public bool ShieldIntact = true;
    [HideInInspector]
    public bool FirstRunHasDash = false;
    [HideInInspector]
    public bool FirstRunHasGloves = false;
    [HideInInspector]
    public bool FirstRunHasShield = false;
    [HideInInspector]
    public bool DidDropSpikes = false;

    public void StartTimer()
    {
        _timerRunning = true;
    }

    public void PauseTimer()
    {
        _timerRunning = false;
    }
    float minutes = 0;
    float seconds = 0;
    float miliseconds = 0;

    private void FixedUpdate()
    {

       

        if (miliseconds >= 60)
        {
            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }
            else if (seconds <= 59)
            {
                seconds++;
            }

            miliseconds = 0;
        }

        miliseconds += Time.deltaTime * 100;

        if (!_timerRunning) return;
        if (null!=_timerImage_Min_Sec)
            _timerImage_Min_Sec.text=(string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00")));
        if (null != _timerImage_Mili)
            _timerImage_Mili.text =  ((int)miliseconds).ToString("00");
    }


}


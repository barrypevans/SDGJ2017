using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Xml.Serialization;

public class MenuManager : MonoBehaviour
{

    List<Text[]> scores = new List<Text[]>();

    List<UserData> scoreData = null;

    public Text userNameInput;
    public GameObject UserInputPanel;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("score-flag") == 1)
        {
            UserInputPanel.SetActive(true);
            PlayerPrefs.SetInt("score-flag", 0);
        }
    }

    void Start()
    {

        for (int i = 0; i < 10; i++)
        {
            scores.Add(new Text[2] {
                GameObject.Find("slot" + i).GetComponent<Text>(),
                GameObject.Find("score" + i).GetComponent<Text>()
            });
        }

        updateScores();

        if (null == scoreData)
            scoreData = new List<UserData>();


    }

    public void setScore(int position, string name, string score)
    {
        scores[position][0].text = name;
        scores[position][1].text = score;
    }


    public void LoadLevel(string s)
    {
        SceneManager.LoadScene(s);
    }

    public List<UserData> loadXML()
    {
        List<UserData> leaderboard = null;
        try
        {
            XmlSerializer serializer = new
            XmlSerializer(typeof(List<UserData>));
            FileStream fs = new FileStream(Application.persistentDataPath + "/LeaderBoard.xml", FileMode.Open);
            leaderboard = (List<UserData>)serializer.Deserialize(fs);
            fs.Dispose();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return leaderboard;
    }

    private void updateScores()
    {
        scoreData = loadXML();

        if (null != scoreData)
            for (int i = 0; i < Mathf.Min(10, scoreData.Count); i++)
                setScore(i, scoreData[i].name, scoreData[i].minutes.ToString("00") + ":" + scoreData[i].seconds.ToString("00") + ":" + scoreData[i].miliseconds.ToString("00"));
    }

    public void SubmitScore()
    {

        if (null == scoreData) return;

        var minutes = PlayerPrefs.GetInt("minutes");
        var seconds = PlayerPrefs.GetInt("seconds");
        var miliseconds = PlayerPrefs.GetInt("miliseconds");

        UserData ud = new UserData(); ;
        ud.name = userNameInput.text;
        ud.seconds = seconds;
        ud.minutes = minutes;
        ud.miliseconds = miliseconds;

        if (scoreData.Count == 0)
        {
            scoreData.Add(ud);
        }
        else
        {
            var inserted = false;
            for (int i = 0; i < Mathf.Min(10, scoreData.Count); i++)
                if (!CompareScores(ud, scoreData[i]))
                {
                    inserted = true;
                    scoreData.Insert(i, ud);
                    break;
                }

            if (!inserted)
                scoreData.Add(ud);
            // if(scoreData.Count>10)
            //scoreData.RemoveRange(10, scoreData.Count-1);
        }
        SaveScores();
        updateScores();

    }

    private void SaveScores()
    {
        if (null == scoreData) return;
        try
        {
            Debug.Log(scoreData.Count);
            XmlSerializer serializer = new
            XmlSerializer(typeof(List<UserData>));
            using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/LeaderBoard.xml"))
            {
                serializer.Serialize(writer, scoreData);
            }
        }
        catch (Exception e) { Debug.Log(e); }

    }

    private bool CompareScores(UserData ud1, UserData ud2)
    {
        if (ud1.minutes > ud2.minutes) return true;
        if (ud1.minutes < ud2.minutes) return false;

        if (ud1.seconds > ud2.seconds) return true;
        if (ud1.seconds < ud2.seconds) return false;


        if (ud1.miliseconds > ud2.miliseconds) return true;
        if (ud1.miliseconds < ud2.miliseconds) return false;

        return false;
    }

}
[System.Serializable]
public struct UserData
{
    public float minutes;
    public float seconds;
    public float miliseconds;
    public string name;
}
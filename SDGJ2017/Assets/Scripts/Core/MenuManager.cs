using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    List<Text[]> scores = new List<Text[]>();

    void Start () {
        
        for (int i = 0; i<10; i++)
        {
            scores.Add(new Text[2] {
                GameObject.Find("slot" + i).GetComponent<Text>(),
                GameObject.Find("score" + i).GetComponent<Text>()
            });
        }
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
}

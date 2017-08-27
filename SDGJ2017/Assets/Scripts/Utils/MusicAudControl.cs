using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudControl : MonoBehaviour {

    public AudioSource[] musicKeeper = new AudioSource[5];
    int currentTrack = 0;
    
    //Only use this function in scenes where 
	void Start () {
        //Add generic sources
        for (int i = 0; i < 4; i++)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = Resources.Load<AudioClip>("Audio/Theme" + i);
            a.volume = 0f;
            a.transform.parent = transform;
            a.loop = true;
            a.Play();

            musicKeeper[i] = a;
        }

        //Add remaining unique source
        /*AudioSource b = new AudioSource();
        b.clip = Resources.Load<AudioClip>("Audio/SecondTheme");
        b.transform.parent = transform;
        b.loop = true;
        musicKeeper[4] = b;*/

        //set initial audio
        musicKeeper[0].volume = 1f;

	}
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
            trackTransition(currentTrack+1);
    }
	

    Coroutine audioTrans;
    //slide between songs
    public void trackTransition(int i)
    {
        if (null != audioTrans)
            StopCoroutine(audioTrans);
        audioTrans = StartCoroutine(transitionSong(i));
    }
    
    //no shit
    IEnumerator transitionSong(int newTrack)
    {
        float t = 0;
        while (t < 4f)
        {
            musicKeeper[currentTrack].volume = Mathf.Lerp(1,0,t/4f);
            musicKeeper[newTrack].volume = Mathf.Lerp(0,1,t/4f);
            t += Time.deltaTime;
            yield return null;
        }

        currentTrack = newTrack;

        yield return null;
    }

    //Audio Overrides
    public void playClip(string name)
    {
        StartCoroutine(clipLife(null, name, 1f));
    }
    public void playClip(string name, float volume)
    {
        StartCoroutine(clipLife(null, name, volume));
    }
    public void playSpatialClip(GameObject g, string name)
    {
        StartCoroutine(clipLife(g, name, 1f));
    }
    public void playSpatialClip(GameObject g, string name, float volume)
    {
        StartCoroutine(clipLife(g, name, volume));
    }

    //Self Disposing
    IEnumerator clipLife(GameObject g, string name, float volume)
    {
        AudioSource a = new AudioSource();
        a.clip = Resources.Load<AudioClip>("Audio/" + name);
        a.volume = volume;
        if (null != g)
        {
            a.spatialBlend = 1;
            a.transform.parent = g.transform;
        }
        else
        {
            a.transform.parent = transform;
        }

        yield return new WaitForSeconds(a.clip.length);

        Destroy(a);

        yield return null;
    }
}

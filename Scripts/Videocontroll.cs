using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videocontroll : MonoBehaviour {
    private VideoPlayer videoPlayer;
    private GameObject videoplay;
    private AudioSource audioS;
   
    
    
    // Use this for initialization
    void Awake() {
        videoplay = GameObject.Find("videoplay");

        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        audioS = GetComponent<AudioSource>();
    }
    void Start () {
        

    }
	// Update is called once per frame
	void Update () {
        
         if (videoplay.activeSelf)
         {
            if (videoPlayer.isPlaying)
                return;
            else
            {
                videoPlayer.Play();
                audioS.Play();
            }
         }
         else
         {
            audioS.Stop();
             videoPlayer.Stop();
        }
         
    }
}

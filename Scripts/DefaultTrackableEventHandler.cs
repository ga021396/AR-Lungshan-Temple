/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    private GameObject videoplay;
    #region PRIVATE_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNTIY_MONOBEHAVIOUR_METHODS
    
    //function to stop all sounds
    void PlayAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS.isPlaying)
                audioS.Stop();
            else
                audioS.Play();
        }
    }
    public void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS.isPlaying)
                audioS.Stop();
        }
    }

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
     
}

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS
    void Awake()
    {
        videoplay = GameObject.Find("videoplay");
    }

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        
        targetName();
      
        //-----------------------------------------------------
   
        //-----------------------------------------------------
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;

        videoplay.SetActive(false);
    }
    //------------------------------------------------------------
    AudioClip myClip = new AudioClip();
    AudioSource mySource;
    private AudioSource[] allAudioSources;

    public void targetName() {
        //SceneManager.LoadScene("Introduce");
             

        GameObject panel;
        panel = GameObject.Find("Panel");
        Animator anim = panel.GetComponent<Animator>();
       
        //éÊìæâÓè–·`ñ ï¿é∑çsìÆ·`

        Text text;
        text = GameObject.Find("History").GetComponent<Text>();
        //éÊìæâÓè–ï∂éö

        UnityEngine.UI.Image img;
        Sprite Myimg;
        img = GameObject.Find("Photo").GetComponent<UnityEngine.UI.Image>();
        //éÊìæö§ï– 

         mySource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        //éÊìæåÍâπ

        if (mTrackableBehaviour.TrackableName == "SideBuilding")
        {
            text.text = "This is where we show you all the amazing stuff that just arrived at our warehouse. Stay up-to-date with all the latest gear right here, or, if you prefer, you can get this same info via our What's New RSS feed or by following @thinkgeekspam.";
            Myimg = Resources.Load<Sprite>("image/IMG2");
            img.sprite = Myimg;
            anim.Play("move");
            //àÀñ⁄ïWçXä∑ö§ï–ãyï∂éö

            myClip = (AudioClip)Resources.Load("audios/audio00");
            mySource.clip = myClip;
            mySource.Play();
            StopAllAudio();
        }

        if (mTrackableBehaviour.TrackableName == "totem")
        {
            text.text = "High Energy Physics - Theory. New submissions. Submissions received from Fri 4 May 18 to Mon 7 May 18, announced Tue, 8 May 18. New submissions; Cross-lists; Replacements.";
            Myimg = Resources.Load<Sprite>("image/images");
            img.sprite = Myimg;
            anim.Play("move");
            //àÀñ⁄ïWçXä∑ö§ï–ãyï∂éö

            myClip = (AudioClip)Resources.Load("audios/audio02");
            mySource.clip = myClip;
            mySource.Play();
            StopAllAudio();
        }
        if (mTrackableBehaviour.TrackableName == "stele") {
            videoplay.SetActive(true);
        }

        }   
    public void onClick() {
        PlayAllAudio();
        /*AudioClip myClip = new AudioClip();
        myClip = (AudioClip)Resources.Load("audio01");
        AudioSource mySource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        mySource.clip = myClip;
        mySource.Play();
        mySource.enabled = false;
    */}
   
    #endregion // PRIVATE_METHODS
}

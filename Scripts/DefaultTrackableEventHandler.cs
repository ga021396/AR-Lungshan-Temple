/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    private GameObject videoplay;
    private GameObject videoplay2;
    private GameObject videoplay3;

    #region PRIVATE_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNTIY_MONOBEHAVIOUR_METHODS

    //-----------語音部分-----------
    public void ToggleAllAudio()
    {   //設allAudioSources為任意的audiosource物件
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)//抓陣列中的所有物件
        {
            if (audioS.isPlaying)   //如果正在播放則停止 否則播放
                audioS.Pause();
            else
                audioS.Play();
        }
    }
    public void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS.isPlaying)   //當播放時則停止
                audioS.Stop();
        }
    }
    //-----------語音部分-----------
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
        videoplay2 = GameObject.Find("videoplay2");
        videoplay3 = GameObject.Find("videoplay3");//抓scene物件videoplay
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

        //-----------------------------------------------------
        targetName();
        //呼叫function
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

        //-----------------------------------
        videoplay.SetActive(false); //當相機丟失目標時videoplay為false
        videoplay2.SetActive(false);
        videoplay3.SetActive(false);
        //-----------------------------------
        if (mTrackableBehaviour.TrackableName == "unityChan_01")
        {
            StopAllAudio();
        }
        if (mTrackableBehaviour.TrackableName == "unityChan_02")
        {
            StopAllAudio();
        }
        if (mTrackableBehaviour.TrackableName == "unityChan_03")
        {
            StopAllAudio();
        }
        if (mTrackableBehaviour.TrackableName == "stickerr")
        {
            aniplay("model", "ryusanji_mawaru");
        }
    }

    //------------------------------------------------------------
    AudioClip myClip = new AudioClip();
    AudioSource mySource;
    private AudioSource[] allAudioSources;
    //------------------------------------------------------------

    public void targetName() {
  
        //判斷掃描到的物件 來抓不同的Source
        switch (mTrackableBehaviour.TrackableName){
            case "sign_1":
                getSource("text/1", "image/1", "audios/1","山門");
                break;
            case "sign_2":
                getSource("text/2", "image/2", "audios/2", "三川殿");
                break;
            case "sign_3":
                getSource("text/3", "image/3", "audios/3", "龍柱");
                break;
            case "sign_4":
                getSource("text/4", "image/4_1", "audios/4", "屋根の型式");
                break;
            case "sign_5":
                getSource("text/5", "image/5_1", "audios/5", "こうちやき");
                break;
            case "sign_6":
                getSource("text/6", "image/6", "audios/6", "観音さま");
                break;
            case "sign_7-1":
                getSource("text/7-1", "image/7-1", "audios/7-1", "かださま");
                break;
            case "sign_7-2":
                getSource("text/7-2", "image/7-2", "audios/7-2", "文昌帝君");
                break;
            case "sign_8-1":
                getSource("text/8-1", "image/8-1", "audios/8-1", "水仙尊王");
                break;
            case "sign_8-2":
                getSource("text/8-2", "image/8-2", "audios/8-2", "媽祖");
                break;
            case "sign_8-3":
                getSource("text/8-3", "image/8-3", "audios/8-3", "註生娘娘");
                break;
            case "sign_9-1":
                getSource("text/9-1", "image/9-1", "audios/9-1", "関羽");
                break;
            case "sign_9-2":
                getSource("text/9-2", "image/9-2", "audios/9-2", "月下老人");
                break;
            case "omairinoshikata":
                videoplay3.SetActive(true); 
                break;
            case "poeuranai":
                videoplay2.SetActive(true);
                break;
            case "omikuji":
                videoplay.SetActive(true);
                break;
            case "unityChan_01":
                unityChan("point1","waitP1", "audios/P1");
                break;
            case "unityChan_02":
                unityChan("point2","waitP2", "audios/P2");
                break;
            case "unityChan_03":
                unityChan("point3","waitP3", "audios/P3");
                break;
            case "stickerr":
                aniplay("model", "ryusanji");
                break;
        }
    }
    public void aniplay(string tag,string ani) {
        GameObject aniobj = GameObject.FindGameObjectWithTag(tag);
        Animator anim = aniobj.GetComponent<Animator>();
        anim.Play(ani);
    }
    public void unityChan(string tag,string ani="WIN00",string aud= "audios/9-2") {
        aniplay(tag, ani);
        //UnityChan動畫及語音
        PlayAudio(aud);
        mySource.Play();
    }
    public void PlayAudio(string aud){
        mySource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        myClip = (AudioClip)Resources.Load(aud);
        mySource.clip = myClip;
    }
    public void onClick() {
        //按按鈕播/停語音
        ToggleAllAudio();
       }
    public void getSource(string txt,string ima,string aud,string tit) {
        //傳參數從resource抓不同的檔案

        GameObject panel;
        panel = GameObject.Find("introduction");
        Animator anim = panel.GetComponent<Animator>();
        anim.Play("move");
        //取得介紹畫面並執行動畫

        Text text;
        text = GameObject.Find("History").GetComponent<Text>();
        TextAsset txtS;
        Text title = GameObject.Find("title").GetComponent<Text>();
        //取得介紹文字

        UnityEngine.UI.Image img;
        Sprite Myimg;
        img = GameObject.Find("Photo").GetComponent<UnityEngine.UI.Image>();
        //取得圖片 

        PlayAudio(aud);
        mySource.Play();
        //取得語音
        //--------------------------
        txtS = Resources.Load(txt) as TextAsset;
        text.text = txtS.text;
        title.text = tit;
        Myimg = Resources.Load<Sprite>(ima);
        img.sprite = Myimg;
        //依目標更換圖片及文字
    }
    #endregion // PRIVATE_METHODS
}

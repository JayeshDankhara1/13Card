using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;


public class SoundAndMusic : MonoBehaviour
{
    public AudioSource SoundAudioSource;
    public AudioSource MusicAudioSource;
    public AudioSource TouchAudioSource;

    [HideInInspector]
    public static SoundAndMusic instance;


    #region Unity Function
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        StartSetData();
    }
    #endregion

    #region Get/Set Data 
    public void StartSetData()
    {
        SetSound_Volume(StaticData.Sound);
        SetMusic_Volume(StaticData.Music);
        
    }
    #endregion 

    #region Sound Function
    public void PlaySound(AudioClip clip)
    {
        SoundAudioSource.clip = null;
        SoundAudioSource.clip = clip;
        SoundAudioSource.Play();
    }


    public void SoundMute(bool mute) 
    {
        StaticData.MuteSound = mute ? 1 : 0;
        SoundAudioSource.mute = mute;
        TouchAudioSource.mute = mute;
    }
    public void SetSound_Volume(float Volume)
    {
        //Debug.Log(Volume);
        StaticData.Sound= Volume;
        StaticData.Touch= Volume;
        SoundAudioSource.volume = Volume;
        TouchAudioSource.volume = Volume;
    }
    #endregion

    #region Music Function
    public void PlayMusic(AudioClip clip)
    {
        MusicAudioSource.clip = null;
        MusicAudioSource.clip = clip;
        MusicAudioSource.Play();
    }

    public void MuiscMute(bool mute) 
    {
        StaticData.MuteMusic = mute ? 1:0;
        MusicAudioSource.mute = mute;
    }

    

    public void SetMusic_Volume(float Volume,bool IsChage=true) 
    {
        if (IsChage)
        {
            StaticData.Music = Volume;
        }
        MusicAudioSource.volume= Volume;
    }
    #endregion

    #region Touch Function
    public void PlayTouch(AudioClip clip)
    {
        TouchAudioSource.clip = null;
        TouchAudioSource.clip = clip;
        TouchAudioSource.Play();
    }


    #endregion


    #region Other Function
    /*public void PauseSoundAndMusic(bool IsPuse)
    {
        if (IsPuse)
        {
            SoundAudioSource.Pause();
            MusicAudioSource.Pause();
            TouchAudioSource.Pause();
        }
        else
        {
            SoundAudioSource.UnPause();
            MusicAudioSource.UnPause();
            TouchAudioSource.UnPause();
        }
    }*/
    #endregion
}

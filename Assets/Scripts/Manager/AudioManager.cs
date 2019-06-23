using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public static void Init()
    {
        if (_instance == null)
        {
            _instance = new AudioManager();
        }
    }

    private readonly GameObject mListener;
    private AudioSource mSource;
    private AudioSource mSoundSource;

    public float AudioVolume
    {
        get { return mSource.volume; }
        set
        {
            if (_instance.mSource != null)
            {
                _instance.mSource.volume = value;
            }
        }
    }

    public float SoundVolume
    {
        get { return mSoundSource.volume; }
        set
        {
            if (_instance.mSoundSource != null)
            {
                _instance.mSoundSource.volume = value;
            }
        }
    }

    public AudioManager()
    {
        mListener = new GameObject("[Auto]Audio Listener", typeof(AudioListener));
        GameObject.DontDestroyOnLoad(mListener);
    }

    public void PlayUiBGM(string audioName, bool loop = false)
    {
        try
        {
            AudioClip clip = GameAsset.Instance.LoadAudio(audioName);
            if (null == mSource)
            {
                mSource = mListener.AddComponent<AudioSource>();
            }
            mSource.clip = clip;
            mSource.loop = loop;
            AudioVolume = GameSetting.AudioVolume;
            mSource.Play();
        }
        catch (Exception ex)
        {
            DebugUtils.LogError("播放音频错误" + ex);
        }
    }

    public void PlaySound(string audioName)
    {
        try
        {
            AudioClip clip = GameAsset.Instance.LoadAudio(audioName);
            mSoundSource = mListener.AddComponent<AudioSource>();
            mSoundSource.clip = clip;
            mSoundSource.loop = false;
            SoundVolume = GameSetting.SoundVolume;
            mSoundSource.Play();
        }
        catch (Exception ex)
        {
            DebugUtils.LogError("播放音效错误" + ex);
        }
    }

    public void UpdateSoundSource()
    {
        if (null != mSoundSource && !mSoundSource.isPlaying)
        {
            GameObject.Destroy(mSoundSource);
        }
    }

    public void DisPose()
    {
        if (_instance == null) return;

    }
}

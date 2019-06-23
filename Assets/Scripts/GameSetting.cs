using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  保存数据到本地
public class GameSetting
{
    public static readonly string AUDIO_VOLUME = "Audio_Volume";
    public static readonly string SOUND_VOLUME = "Sound_Volume";

    public static float AudioVolume
    {
        get { return PlayerPrefs.GetFloat(AUDIO_VOLUME, 0.5f); }
        set { PlayerPrefs.SetFloat(AUDIO_VOLUME, value); }
    }

    public static float SoundVolume
    {
        get { return PlayerPrefs.GetFloat(SOUND_VOLUME, 0.5f); }
        set { PlayerPrefs.SetFloat(SOUND_VOLUME, value); }
    }
}

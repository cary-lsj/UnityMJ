using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UiInit("DialogSet")]
public class DialogSet : Widget
{
    [UiName("黑 背景")]
    private Button mBigImage;

    [UiName("音乐 音量")]
    private Slider mAudioVloume;
    [UiName("音效 音量")]
    private Slider mSoundVloume;

    protected override void OnStart()
    {
        mBigImage.onClick.AddListener(() => { Close(); });

        mAudioVloume.onValueChanged.AddListener(OnAudioChange);

        mSoundVloume.onValueChanged.AddListener(OnSoundChange);

        InitData();
    }

    private void InitData()
    {
        mAudioVloume.value = GameSetting.AudioVolume;
        mSoundVloume.value = GameSetting.SoundVolume;
    }

    private void OnAudioChange(float value)
    {
        AudioManager.Instance.AudioVolume = value;
        GameSetting.AudioVolume = value;
    }

    private void OnSoundChange(float value)
    {
        AudioManager.Instance.SoundVolume = value;
        GameSetting.SoundVolume = value;
    }
}

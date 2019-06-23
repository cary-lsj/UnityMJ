using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLogin : StateBase
{
    protected override void Start(params object[] args)
    {
        AudioManager.Instance.PlayUiBGM(GameAsset.AUDIO_SYSTEM + Constants.bg_music0, true);
        GameUI.Instance.CreatePanel<PageLogin>(false);
    }
}

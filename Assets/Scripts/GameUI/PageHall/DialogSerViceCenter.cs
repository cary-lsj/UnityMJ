using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UiInit("DialogSerViceCenter")]
public class DialogSerViceCenter : Widget
{
    [UiName("黑 背景")]
    private Button mBigBlack;

    protected override void OnStart()
    {
        base.OnStart();

        mBigBlack.onClick.AddListener(() => { Close(); });
    }
}

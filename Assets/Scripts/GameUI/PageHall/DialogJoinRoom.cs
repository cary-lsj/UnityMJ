using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UiInit("DialogJoinRoom")]
public class DialogJoinRoom : Widget
{
    [UiName("黑 背景")]
    private Button mBigImage;

    protected override void OnStart()
    {
        base.OnStart();

        mBigImage.onClick.AddListener(
            () =>
            {
                Close();
            });
    }
}

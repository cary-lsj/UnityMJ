using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UiInit("DialogAnnouncement")]
public class DialogAnnouncement : Widget
{
    [UiName("黑 背景")]
    public Button BigImage;

    protected override void OnStart()
    {
        base.OnStart();

        BigImage.onClick.AddListener(
            () =>
            {
                Close();
            });
    }
}

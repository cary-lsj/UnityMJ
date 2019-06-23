using System.Collections;
using System.Collections.Generic;
using ProtoMsg;
using UnityEngine;
using UnityEngine.UI;

[UiInit("PageMall")]
public class PageMall : Widget
{
    [UiName("返回 节点")] public Button Back;

    protected override void OnStart()
    {
        Back.onClick.AddListener(
            () =>
            {
                GameCore.Instance.Back();
            });
    }
}

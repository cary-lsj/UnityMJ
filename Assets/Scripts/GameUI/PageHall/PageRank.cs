using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UiInit("PageRank")]
public class PageRank : Widget
{
    [UiName("返回")]
    private Button mBack;

    protected override void OnStart()
    {
        base.OnStart();

        mBack.onClick.AddListener(() => { GameCore.Instance.Back(); });
    }

}

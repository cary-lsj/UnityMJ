using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProtoMsg;

[UiInit("PageGame")]
public class PageGame : Widget
{
    protected override void OnStart()
    {
        base.OnStart();

        GameInit();
    }

    private void GameInit()
    {
    }
}

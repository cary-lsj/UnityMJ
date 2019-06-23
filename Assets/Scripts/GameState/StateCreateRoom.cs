using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCreateRoom : StateBase
{
    protected override void Start(params object[] args)
    {
        base.Start(args);

        var gameType = (GameType) args[0];

        GameUI.Instance.CreatePanel<PageCreateRoom>(gameType, false);
    }

}

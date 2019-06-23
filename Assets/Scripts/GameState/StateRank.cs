using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRank : StateBase
{
    protected override void Start(params object[] args)
    {
        base.Start();
        GameUI.Instance.CreatePanel<PageRank>();
    }
}

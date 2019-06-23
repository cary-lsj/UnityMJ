using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMall : StateBase
{
    protected override void Start(params object[] args)
    {
        GameUI.Instance.CreatePanel<PageMall>();
    }

}

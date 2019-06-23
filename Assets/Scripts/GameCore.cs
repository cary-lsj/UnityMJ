using System;
using System.Collections;
using System.Collections.Generic;
using ProtoMsg;
using UnityEngine;

//  此类对整体游戏进行管理
public class GameCore
{
    private static GameCore _instance;
    public static GameCore Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new GameCore();
            }
            return _instance;
        }
    }

    public StateBase CurrentState
    {
        get
        {
            return mCurrentState;
        }
    }

    private StateBase mCurrentState;
    Stack<StateBase> mStateStack = new Stack<StateBase>();

    public void Init(out Action update, out Action lateUpdate)
    {
        update = Update;
        lateUpdate = LateUpdate;
    }

    public void InitCore()
    {
        InitManagers();
    }

    public void Update()
    {
        GameNet.Main.MainUpdate();
        GameNet.GameBattle.BattleUpdate();
        GameUI.Update();
        mCurrentState.Update();
        GameBase.UpdateAll();
        AudioManager.Instance.UpdateSoundSource();
    }

    public void LateUpdate()
    {
        if (mCurrentState != null)
        {
            mCurrentState.LateUpdate();
        }
        GameUI.LateUpdate();
    }

    public void InitManagers()
    {
        PlayerManager.Init();
        RoomManager.Init();
        ConfigManager.Init();
    }

    public void DisposeManagers()
    {
        PlayerManager.Dispose();
        RoomManager.Dispose();
        ConfigManager.Dispose();
    }

    public void SetState(GameState gameState, params object[] args)
    {
        DebugUtils.LogWarning("SetState" + gameState);
        if (mCurrentState != null)
        {
            mCurrentState.Quit();
        }
        GameUI.Clear();

        EnterState(gameState, args);
    }

    public void PushState(GameState state, params object[] args)
    {
        if (mCurrentState != null)
        {
            mCurrentState.OnDisable();
            mStateStack.Push(mCurrentState);
            GameUI.Instance.SavePanels(mCurrentState.State);
            DebugUtils.LogWarning("Push State:" + mCurrentState.State);
        }

        EnterState(state, args);
    }

    public void PopState()
    {
        DebugUtils.LogWarning("PopState");
        if (mStateStack.Count == 0)
        {
            SetState(GameState.Hall);
        }
        else
        {
            mCurrentState.Quit();
            mCurrentState = mStateStack.Pop();
            GameUI.Instance.LoadPanels(mCurrentState.State, true);
        }
    }

    public void EnterState(GameState state, params object[] args)
    {
        mCurrentState = StateBase.New(state);

        if (mCurrentState != null)
        {
            mCurrentState.OnStart(args);
        }
    }

    public void PopAllState(GameState state,params object[] args)
    {//  可以用于直接回到PageHall
        DebugUtils.Log("PopAllState" + state);

        while (mStateStack.Count > 0)
        {
            if (mStateStack.Peek().State == state)
            {
                PopState();
                return;
            }
            var State = mStateStack.Pop();
            GameUI.Instance.LoadPanels(State.State, false);
            State.Quit();
        }

        SetState(state, args);
    }

    public void Back()
    {
        Instance.PopState();
    }
}

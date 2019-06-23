using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Laoding,    //  加载
    Login,      //  登录
    Hall,       //  游戏大厅
    Mall,       //  商城
    Rank,       //  排行
    CreateRoom  //  创建房间
}

public class StateBase
{
    public static StateBase New(GameState state)
    {
        StateBase gameState;
        switch (state)
        {
            case GameState.Login:
                gameState = new StateLogin();
                break;
            case GameState.Hall:
                gameState = new StateHall();
                break;
            case GameState.Mall:
                gameState = new StateMall();
                break;
            case GameState.Rank:
                gameState = new StateRank();
                break;
            case GameState.CreateRoom:
                gameState = new StateCreateRoom();
                break;
            default:
                return null;
        }
        gameState.State = state;
        return gameState;
    }

    public GameState State { get; private set; }

    public void OnStart(params object[] args)
    {
        Start(args);
    }

    protected virtual void Start(params object[] args)
    {

    }

    public virtual void Quit()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void OnDisable()
    {

    }

    public virtual void OnApplicationPause(bool paused)
    {

    }

    //public virtual IEnumerator OnEnable()
    //{
    //    yield return null;
    //}
}

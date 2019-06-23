using System;
using ProtoMsg;
using UnityEngine;

//  此类做游戏整体的一些初始化
class GameMain : MonoBehaviour
{
    public Constants.ServerType ServerType;
    public Constants.Route Route;
    public int Port = Constants.Server.ServerPort; 

    private Action mUpdate;
    private Action mLateUpdate;
    private string mHost;
    private string mRoute;
    
    void Awake()
    {
        GameCore.Instance.Init(out mUpdate, out mLateUpdate);
        GameCore.Instance.InitCore();
        AudioManager.Init();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        StartGame();
    }
        
    void Update()
    {
        UpdateFPS();
        mUpdate();
    }

    void LateUpdate()
    {
        mLateUpdate();
    }

    private void OnGUI()
    {
#if !UNITY_IOS
        //DisplayFPS();
#endif
    }

    private void StartGame()
    {
        SetUrl();
        GameCore.Instance.SetState(GameState.Login);
    }

    private void SetUrl()
    {
        switch (ServerType)
        {
            case Constants.ServerType.Internet:
                mHost = Constants.Server.Internet_Server;
                break;
            case Constants.ServerType.Intranet:
                mHost = Constants.Server.Intranet_Server;
                break;
        }

        switch (Route)
        {
            case Constants.Route.index:
                mRoute = Constants.RouteValue.Index;
                break;
            case Constants.Route.game:
                mRoute = Constants.RouteValue.Game;
                break;
            case Constants.Route.login:
                mRoute = Constants.RouteValue.Login;
                break;
            case Constants.Route.loginWX:
                mRoute = Constants.RouteValue.LoginWX;
                break;
        }
        GameNet.Main.Init(mHost, Port);
    }

    #region FPS
    private float mCurrentFPS;
    private float mFpsTimer;
    private float mAvgFps;
    private float mTotalFps;
    private int mFpsCounter;
    private int mAvgFpsCounter;

    private void UpdateFPS()
    {
        ++mFpsCounter;
        float time = Time.time - mFpsTimer;
        if (time > 1f)
        {
            mCurrentFPS = mFpsCounter / time;
            ++mAvgFpsCounter;
            mTotalFps += mCurrentFPS;
            mAvgFps = mTotalFps / mAvgFpsCounter;

            mFpsCounter = 0;
            mFpsTimer = Time.time;
        }
    }

    Rect Rect = new Rect(0, 0, 100, 30);
    GUIStyle bb;
    void DisplayFPS()
    {
        if (bb == null)
        {
            bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = Color.green;
            bb.fontSize = 30;
        }
        GUI.Label(Rect, string.Format("{0:N1}/{1:N1}", mCurrentFPS, mAvgFps), bb);
    }
    #endregion
}


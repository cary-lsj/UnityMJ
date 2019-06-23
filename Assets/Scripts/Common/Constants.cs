using System;

public class Constants
{
    public enum ServerType
    {//  内网、外网
        Internet,
        Intranet
    }

    public enum Route
    {//  路由
        index,
        game,
        login,
        loginWX
    }

    public class Server
    {//  服务地址
        public static readonly string Internet_Server = "47.92.157.12";
        public static readonly string Intranet_Server = "192.168.0.100";
        public static readonly int ServerPort = 8001;
    }

    public class RouteValue
    {//  路由
        public static readonly string Index = "index";
        public static readonly string Game = "game";
        public static readonly string Login = "login";
        public static readonly string LoginWX = "loginWX";
        public static readonly string LoginHeart = "loginheart";
    }

    #region AudioClip名称
    public const string bg_music0 = "bg_music0";
    public const string g_clock = "g_clock";
    public const string g_gamerStar = "g_gamerStar";
    public const string g_loss = "g_loss";
    public const string g_putPaiDown = "g_putPaiDown";
    public const string g_screenshot = "g_screenshot";
    public const string g_win = "g_win";

    #endregion
}

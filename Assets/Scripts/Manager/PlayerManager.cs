using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoMsg;

public class PlayerManager
{
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public int mExp;         //  用户经验值
    public int mGold;        //  用户游戏币
    public int mMoney;       //  用户钻石
    public int mParent;      //  用户引荐者
    public int mGender;      //  用户性别
    public int mUserId;      //  用户id
    public int mWinCount;    //  用户胜利局数
    public int mTotalCount;  //  用户总局数

    public string mIP;       //  用户远程Ip
    public string mRoom;     //  用户当前所在房间（断线重连）
    public string mNick;     //  用户昵称
    public string mName;     //  用户名称
    public string mICode;    //  用户引荐码
    public string mPhone;    //  用户电话
    public string mAssets;   //  用户资产
    public string mHeadimg;  //  用户头像
    public string mRecords;  //  用户对局记录

    public bool bLuckyToday = false;      //  当天是否已经玩过大转盘
    public bool bWelfareToday = false;    //  当天是否已经领过福利
    public bool bShareAwardWeek = false;  //  本周是否已经领过分享奖励

    public List<bool> bRkAwardFlags;      //  用户是否领过排行奖励标志[日，周，月，年]

    public static void Init()
    {
        _instance = new PlayerManager();
    }

    public void SetUserInfo(ProtoMsg.User user)
    {
        _instance.mExp = user.nExp;
        _instance.mGold = user.nGold;
        _instance.mMoney = user.nMoney;
        _instance.mParent = user.nParent;
        _instance.mGender = user.nGender;
        _instance.mUserId = user.nUserID;
        _instance.mWinCount = user.nWinCount;
        _instance.mTotalCount = user.nTotalCount;

        _instance.mIP = user.sIP;
        _instance.mRoom = user.sRoom;
        _instance.mNick = user.sNick;
        _instance.mName = user.sName;
        _instance.mICode = user.sICode;
        _instance.mPhone = user.sPhone;
        _instance.mAssets = user.sAssets;
        _instance.mHeadimg = user.sHeadimg;
        _instance.mRecords = user.sRecords;

        _instance.bLuckyToday = user.bLuckyToday;
        _instance.bWelfareToday = user.bWelfareToday;
        _instance.bShareAwardWeek = user.bShareAwardWeek;

        _instance.bRkAwardFlags = user.bRkAwardFlags;
    }

    public static void Dispose()
    {
        if (_instance != null)
        {
            _instance.bRkAwardFlags.Clear();
            _instance.bRkAwardFlags = null;
        }
        _instance = null;
    }
}

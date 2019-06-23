using System.Collections;
using System.Collections.Generic;
using ProtoMsg;
using UnityEngine;
using LitJson;
using UnityEngine.UI;


[UiInit("PageCreateRoom")]
public class PageCreateRoom : Widget
{
    [UiName("返回 按钮")]
    private Button mBack;

    [UiName("哈尔滨玩法")]
    private GameObject mHarbin;
    [UiName("红中当宝")]
    private Toggle mHZDangBao;
    [UiName("红中算色")]
    private Toggle mHZSuanSe;
    [UiName("刮大风")]
    private Toggle mGuaDaFeng;
    [UiName("对宝")]
    private Toggle mDuiBao;
    [UiName("庄家翻倍")]
    private Toggle mZZFanBei;
    [UiName("边胡")]
    private Toggle mBianHu;
    [UiName("单吊算夹")]
    private Toggle mDDSuanJia;
    [UiName("一炮多响")]
    private Toggle mYPDuoXiang;

    [UiName("牡丹江玩法")]
    private GameObject mMuDanRiver;
    [UiName("M一炮多响")]
    private Toggle mYPDuoXiang1;
    [UiName("M对宝")]
    private Toggle mDuiBao1;
    [UiName("M下蛋")]
    private Toggle mXiaDan;
    [UiName("M红中满天飞")]
    private Toggle mHZMTF;
    [UiName("M刮大风")]
    private Toggle mGuaDaFeng1;
    [UiName("M大扣")]
    private Toggle mDaKou;
    [UiName("M庄翻倍")]
    private Toggle mZhuangFanBei;

    [UiName("胡死夹")]
    private Toggle mHuSiJia;
    [UiName("传统胡法")]
    private Toggle mTraditionHu;

    [UiName("四人")]
    private Toggle mFourNum;
    [UiName("三人")]
    private Toggle mThreeNum;
    [UiName("二人")]
    private Toggle mTwoNum;

    [UiName("四圈")]
    private Toggle mFourRound;
    [UiName("八圈")]
    private Toggle mEightRound;
    [UiName("十二圈")]
    private Toggle mTwelveRound;

    [UiName("钻石消耗")]
    private Text mConsume;

    [UiName("创建房间 按钮")]
    private Button mCreateRoom;

    private GameType mGameType;

    private JsonData mHarbinConfig;
    private JsonData mMuDanRiverConfig;

    private List<int> mOptionsPlay = new List<int>();

    private int mHhzdb;  //  红中当宝
    private int mHhzss;  //  红中算色
    private int mHgdf;   //  刮大风
    private int mHdb;    //  对宝
    private int mHzzfb;  //  庄家翻倍
    private int mHbh;    //  边胡
    private int mHddsj;  //  单吊算夹
    private int mHypdx;  //  一炮多响

    private int mMypdx;  //  一炮多响
    private int mMdb;    //  对宝
    private int mMxd;    //  下蛋
    private int mMhzmtf; //  红中满天飞
    private int mMgdf;   //  刮大风
    private int mMdk;    //  大扣
    private int mMzfb;   //  庄翻倍

    private int mGamePlayType;  //  玩法大类型
    private string mCradType;
    private int mPlayers;

    protected override void OnStart()
    {
        mGameType = (GameType) mParm;

        mHarbin.SetActive(mGameType == GameType.Harbin);
        mMuDanRiver.SetActive(mGameType == GameType.MuDanRiver);

        mBack.onClick.AddListener(() =>
        {
            GameCore.Instance.Back();
        });

        mCreateRoom.onClick.AddListener(() =>
        {
            CreateRoom();
        });

        #region 玩法选择

        mHZDangBao.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mHhzdb);
        });
        mHZSuanSe.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mHhzss);
        });
        mGuaDaFeng.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mHgdf);
        });
        mDuiBao.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mHdb);
        });
        mZZFanBei.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mHzzfb);
        });
        mBianHu.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mHbh);
        });
        mDDSuanJia.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mHddsj);
        });
        mYPDuoXiang.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mHypdx);
        });

        mYPDuoXiang1.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mMypdx);
        });
        mDuiBao1.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mMdb);
        });
        mXiaDan.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mMxd);
        });
        mHZMTF.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mMhzmtf);
        });
        mGuaDaFeng1.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mMgdf);
        });
        mDaKou.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mMdk);
        });
        mZhuangFanBei.onValueChanged.AddListener(evt =>
        {
            SetGamePlayType(evt, mMzfb);
        });

        #endregion

        mHuSiJia.onValueChanged.AddListener(evt =>
        {
            if (evt)
            {
                if (mGameType == GameType.Harbin)
                    mGamePlayType = (int)mHarbinConfig["jiahu"];
                else
                    mGamePlayType = (int) mMuDanRiverConfig["jiahu"];
            }
        });
        mTraditionHu.onValueChanged.AddListener(evt =>
        {
            if (evt)
            {
                mOptionsPlay.Clear();
                SetTraditionPlay();
                if (mGameType == GameType.Harbin)
                    mGamePlayType = (int) mHarbinConfig["chuantong"];
                else
                    mGamePlayType = (int) mMuDanRiverConfig["chuantong"];
            }
        });

        #region 人数
        mFourNum.onValueChanged.AddListener(evt =>
        {
            if (evt)
                mPlayers = 4;
        });
        mThreeNum.onValueChanged.AddListener(evt =>
        {
            if (evt)
                mPlayers = 3;
        });
        mTwoNum.onValueChanged.AddListener(evt =>
        {
            if (evt)
                mPlayers = 2;
        });
        #endregion

        #region 圈数
        mFourRound.onValueChanged.AddListener(evt =>
        {
            mCradType = "1";
        });
        mEightRound.onValueChanged.AddListener(evt =>
        {
            mCradType = "2";
        });
        mTwelveRound.onValueChanged.AddListener(evt =>
        {
            mCradType = "3";
        });
        #endregion

        InitData();
    }

    private void InitData()
    {
        

        if (!ConfigManager.Instance.ConfigData.TryGetValue(ConfigKey.haerbin, out mHarbinConfig))
        {
            DebugUtils.LogError("没有找到哈尔滨玩法配置！");
            return;
        }

        if (!ConfigManager.Instance.ConfigData.TryGetValue(ConfigKey.mudanjiang, out mMuDanRiverConfig))
        {
            DebugUtils.LogError("没有找到哈尔滨玩法配置！");
            return;
        }

        #region 哈尔滨玩法配置数据

        mHhzdb = (int) mHarbinConfig["hongzhongbao"];
        mHhzss = (int) mHarbinConfig["hongzhong"];
        mHgdf = (int) mHarbinConfig["dafeng"];
        mHdb = (int) mHarbinConfig["duibao"];
        mHzzfb = (int) mHarbinConfig["zhuang"];
        mHbh = (int) mHarbinConfig["bianhu"];
        mHddsj = (int) mHarbinConfig["dandiao"];
        mHypdx = (int) mHarbinConfig["duoxiang"];

        #endregion

        #region 牡丹江玩法配置数据

        mMypdx = (int) mMuDanRiverConfig["duoxiang"];
        mMdb = (int) mMuDanRiverConfig["duibao"];
        mMxd = (int) mMuDanRiverConfig["xiadan"];
        mMhzmtf = (int) mMuDanRiverConfig["hongzhong"];
        mMgdf = (int) mMuDanRiverConfig["dafeng"];
        mMdk = (int) mMuDanRiverConfig["dakou"];
        mMzfb = (int) mMuDanRiverConfig["zhuang"];

        #endregion

        if (mGameType == GameType.Harbin)
        {//  初始化数据
            mConsume.text = "X2";
            mGamePlayType = (int)mHarbinConfig["jiahu"];
        }
        else
        {
            mGamePlayType = (int)mMuDanRiverConfig["jiahu"];
            mConsume.text = "X4";
        }

        mPlayers = 4;
        mCradType = "1";
    }

    private void SetGamePlayType(bool b,int type)
    {
        if (b)
        {
            mHuSiJia.isOn = true;
            mTraditionHu.isOn = false;
            mOptionsPlay.Add(type);
        }
        else
            DelPlayes(type);
    }

    private void DelPlayes(int playType)
    {
        foreach (var val in mOptionsPlay)
        {
            if (val == playType)
            {
                mOptionsPlay.Remove(val);
                break;
            }
        }
    }

    private void SetTraditionPlay()
    {
        mHZDangBao.isOn = false;
        mHZSuanSe.isOn = false;
        mGuaDaFeng.isOn = false;
        mDuiBao.isOn = false;
        mZZFanBei.isOn = false;
        mBianHu.isOn = false;
        mDDSuanJia.isOn = false;
        mYPDuoXiang.isOn = false;

        mYPDuoXiang1.isOn = false;
        mDuiBao1.isOn = false;
        mXiaDan.isOn = false;
        mHZMTF.isOn = false;
        mGuaDaFeng1.isOn = false;
        mDaKou.isOn = false;
        mZhuangFanBei.isOn = false;
    }

    private void CreateRoom()
    {
        GameNet.GameBattle.Connect(Constants.Server.Internet_Server, Constants.Server.ServerPort, OnConnected);
    }

    private void OnConnected()
    {
        GamePlay gp = new GamePlay();
        gp.nType = mGamePlayType;
        gp.optionals.AddRange(mOptionsPlay);

        GameNet.GameBattle.Send("CreateRoomRequest", new Msg()
        {
            type = EnumMsg.createroomrequest,
            request = new Request()
            {
                createRoomRequest = new CreateRoomRequest()
                {
                    nUserID = PlayerManager.Instance.mUserId,
                    sCardType = mCradType,
                    gamePlay = gp,
                    nPlayers = mPlayers
                }
            }
        });
    }
}

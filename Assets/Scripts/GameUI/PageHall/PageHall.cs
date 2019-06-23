using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum GameType
{
    Harbin,
    MuDanRiver
}

[UiInit("PageHall")]
public class PageHall : Widget
{
    [UiName("用户 昵称")]
    private Text mPlayerName;
    [UiName("用户 ID")]
    private Text mPlayerId;
    [UiName("用户 头像")]
    private Image mPlayerHead;

    [UiName("钻石 数量")]
    private Text mDiamond;
    [UiName("金币 数量")]
    private Text mGodCoin;
    [UiName("加 钻石")]
    private Button mDiamonAdd;

    [UiName("加入 房间")]
    private Button mJoinRoom;

    [UiName("哈尔滨 麻将")]
    private Button mHarbin;
    [UiName("牡丹江 麻将")]
    private Button mMuDanRiver;
    [UiName("欢乐大众场")]
    private Button mJoyRoom;

    [UiName("邮件")]
    private Button mMail;
    [UiName("公告")]
    private Button mAnnouncement;
    [UiName("排行")]
    private Button mRank;
    [UiName("客服 中心")]
    private Button mServerCenter;
    [UiName("设置 节点")]
    private Button mSet;
    [UiName("商店 节点")]
    private Button mMall;

    protected override void OnEnable()
    {
        base.OnEnable();
        Refresh();
    }

    protected override void OnStart()
    {
        mJoinRoom.onClick.AddListener(() =>
        {
            GameUI.Instance.CreatePanel<DialogJoinRoom>(null, true);
        });

        mHarbin.onClick.AddListener(() =>
        {
            GameCore.Instance.PushState(GameState.CreateRoom, GameType.Harbin);
        });

        mMuDanRiver.onClick.AddListener(() =>
        {
            GameCore.Instance.PushState(GameState.CreateRoom, GameType.MuDanRiver);
        });

        mJoyRoom.onClick.AddListener(() =>
        {
            //GameCore.Instance.PushState();
        });

        mMail.onClick.AddListener(() =>
        {
            GameUI.Instance.CreatePanel<DialogMail>(null, true);
        });

        mAnnouncement.onClick.AddListener(() =>
        {
            GameUI.Instance.CreatePanel<DialogAnnouncement>(null, true);
        });

        mRank.onClick.AddListener(() =>
        {
            GameCore.Instance.PushState(GameState.Rank);
        });

        mServerCenter.onClick.AddListener(() =>
        {
            GameUI.Instance.CreatePanel<DialogSerViceCenter>(null, true);
        });

        mMall.onClick.AddListener(() =>
        {
            GameCore.Instance.PushState(GameState.Mall);
        });

        mSet.onClick.AddListener(() =>
        {
            GameUI.Instance.CreatePanel<DialogSet>(null, true);
        });

        mDiamonAdd.onClick.AddListener(() =>
        {
            GameCore.Instance.PushState(GameState.Mall);
        });

        InitData();
        Refresh();
    }

    private void Refresh()
    {
        mDiamond.text = PlayerManager.Instance.mMoney.ToString();
        mGodCoin.text = PlayerManager.Instance.mGold.ToString();
    }

    private void InitData()
    {
        StartCoroutine(GameNet.Main.GetTexture(PlayerManager.Instance.mHeadimg, tex =>
        {//  设置用户头像（网络）
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
            mPlayerHead.sprite = sprite;
        }));
        mPlayerName.text = PlayerManager.Instance.mNick;
        mPlayerId.text = PlayerManager.Instance.mUserId.ToString();
    }
}

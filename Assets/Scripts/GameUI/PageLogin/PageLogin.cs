using System;
using System.Collections;
using System.Collections.Generic;
using ProtoMsg;
using UnityEngine.Networking;
using UnityEngine.UI;

[UiInit("PageLogin")]
public class PageLogin : Widget
{
    [UiName("用户登陆")]
    public Button UserLogin;

    public Button TouristLogin; //  游客登陆
    
    public string openid="1234";

    protected override void OnStart()
    {
        UserLogin.onClick.AddListener(() => { UserLoginRequest(); });
    }

    private void UserLoginRequest()
    {
        DebugUtils.Log("用户登陆");
        GameUI.Instance.CreatePanel<DialogLogin>(null, true);
    }

    private void WXLoginRequest()
    {
        DebugUtils.Log("微信登陆测试");
        StartCoroutine(GameNet.Main.HttpRequest(Constants.RouteValue.LoginWX, "LoginWXRequest", new Msg()
        {
            type = EnumMsg.loginwxrequest,
            request = new Request()
            {
                loginWXRequest = new LoginWXRequest()
                {
                    sOpenID = openid
                }
            }
        }, OnLoginResponse));
    }

    private void OnLoginResponse(byte[] data)
    {
        var msg = GameNet.Deserialize<Msg>(data);

        if (null != msg.response)
        {
            if (!GameNet.Main.ResponseError(msg.response.loginResponse.nErrorCode))
            {
                DebugUtils.Log("UserId:" + msg.response.loginResponse.requester.nUserID);
                PlayerManager.Instance.SetUserInfo(msg.response.loginResponse.requester);
            }
        }
        else
        {
            DebugUtils.LogWarning("OnLoginResponse msg is null!");
        }
    }
}

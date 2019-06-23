using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProtoMsg;

[UiInit("DialogLogin")]
public class DialogLogin : Widget
{
    [UiName("黑 背景")]
    private Button mBigImage;

    [UiName("账号")]
    private InputField mAccount;

    [UiName("密码")]
    private InputField mPassword; 

    [UiName("登陆 节点")]
    private Button mLoginBtn;

    protected override void OnStart()
    {
        mBigImage.onClick.AddListener(
            () =>
            {
                Close();
            });

        mLoginBtn.onClick.AddListener(() => { Login(); });
    }

    private void Login()
    {
        var account = mAccount.text;
        var password = mPassword.text;

        if (account == string.Empty || password == string.Empty)
        {
            DebugUtils.LogError("用户名或密码为空！");
            return;
        }

        StartCoroutine(GameNet.Main.HttpRequest(Constants.RouteValue.Login, "LoginRequest", new Msg()
        {
            type = EnumMsg.loginrequest,
            request = new Request()
            {
                loginRquest = new LoginRequest()
                {
                    sName = account,
                    sPassWord = password
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
                GameCore.Instance.SetState(GameState.Hall, null);
            }
            else
            {
                DebugUtils.LogWarning("OnLoginResponse msg is null!");
            }
        }
    }
}

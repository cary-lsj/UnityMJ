using ProtoMsg;
using ProtoBuf;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System;
using System.Net;
using UnityEngine.Networking;
using LitJson;
using UnityEngine;

public class GameNet
{

    private static GameNet _main;

    public static GameNet Main
    {
        get
        {
            if (_main == null)
            {
                _main = new GameNet();
            }
            return _main;
        }
    }

    private static GameNet _gameBattle;

    public static GameNet GameBattle
    {
        get
        {
            if (_gameBattle == null)
            {
                _gameBattle = new GameNet();
            }
            return _gameBattle;
        }
    }

    public Socket BattleSocket
    {
        get { return _gameBattle.mSocket; }
    }

    private JsonData mconfigData;

    private Socket mSocket;
    private Action mConnectCallback;
    private byte[] mReceiveData = new byte[1024];
    private byte[] mSendData;

    private string mUrl;

    public void Init(string strHost, int uPort)
    {
        mUrl = "http://" + strHost + ":" + uPort + "/" ;

        if (!ConfigManager.Instance.ConfigData.TryGetValue(ConfigKey.nErrorCode, out mconfigData))
        {
            DebugUtils.LogError("配置文件缺少错误码！");
        }
    }

    public void MainUpdate()
    {

    }

    public void BattleUpdate()
    {
        if (_gameBattle.IsConnect())
        {
            Recieve();
        }
    }

    public bool IsConnect()
    {
        return mSocket == null ? false : mSocket.Connected;
    }

    MemoryStream msgStream = new MemoryStream();
    public IEnumerator HttpRequest(string route,string requestType, IExtensible msg, Action<byte[]> callBack)
    {
        if (this != Main)
        {
            yield break;
        }
        DebugUtils.LogWarning(string.Format("协议请求{0}", requestType));
        msgStream.SetLength(0);
        Serializer.Serialize<IExtensible>(msgStream, msg);
        var unityWebRequest = UnityWebRequest.Put(mUrl + route, msgStream.ToArray());
        unityWebRequest.method = UnityWebRequest.kHttpVerbPOST;
        unityWebRequest.SendWebRequest();
        while (!unityWebRequest.isDone)
        {
            yield return unityWebRequest;
        }

        if (unityWebRequest.responseCode != 200)
        {
            DebugUtils.LogWarning("unityWebRequest error! seate:" + unityWebRequest.responseCode);
            yield break;
        }
        callBack(unityWebRequest.downloadHandler.data);
        unityWebRequest.Dispose();
    }

    public IEnumerator GetTexture(string uri,Action<Texture2D> callBack)
    {
        var webRequestTexture = UnityWebRequestTexture.GetTexture(uri);
        webRequestTexture.SendWebRequest();
        while (!webRequestTexture.isDone)
        {
            yield return webRequestTexture;
        }

        if (webRequestTexture.responseCode != 200)
        {
            DebugUtils.LogWarning("unityWebRequest error! seate:" + webRequestTexture.responseCode);
            yield break;
        }

        Texture2D texture = new Texture2D(512, 512);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.LoadImage(webRequestTexture.downloadHandler.data);
        callBack(texture);
    }

    public bool ResponseError(int errorCode)
    {
        if (errorCode != 0)
        {
            foreach (var val in mconfigData.Keys)
            {
                var value = mconfigData[val];
                if (int.Parse(value.ToString()) == errorCode)
                {
                    DebugUtils.LogError("NetWork Error" + val);
                }
                return true;
            }
        }
        return false;
    }

    //public void GameConnect()
    //{
    //    NetworkClient networkClient = new NetworkClient();
    //    networkClient.connection = 
    //}

    public void Connect(string strHost, int uPort, Action callBack)
    {
        if (this != _gameBattle)
            return;

        mConnectCallback += callBack;
        IPAddress[] addresses = Dns.GetHostAddresses(strHost);
        if (addresses.Length == 0)
        {
            return;
        }
        foreach (var address in addresses)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                break;
            }
        }
        mSocket.BeginConnect(strHost, uPort, ConnectCallBack, mSocket);
    }

    void ConnectCallBack(IAsyncResult iar)
    {
        try
        {
            mSocket.EndConnect(iar);
            mConnectCallback();
        }
        catch (Exception e)
        {
            DebugUtils.LogError(e.ToString());
        }
    }

    public void Send(string requestType, IExtensible msg)
    {
        if (this != _gameBattle)
        {
            return;
        }
        msgStream.SetLength(0);
        Serializer.Serialize(msgStream, msg);
        if (_gameBattle.IsConnect())
        {
            try
            {
                mSocket.Send(msgStream.ToArray());
            }
            catch (Exception e)
            {
                DebugUtils.LogError(e.ToString());
            }
        }
    }

    public void Recieve()
    {
        mSocket.Receive(mReceiveData);

        var msg = GameNet.Deserialize<Msg>(mReceiveData);
        if (null != msg)
        {
        }
    }

    public static T Deserialize<T>(byte[] data)
    {
        return Serializer.Deserialize<T>(new MemoryStream(data));
    }

    public void Close()
    {
        if (mSocket != null)
        {
            mSocket.Close();
            mSocket = null;
        }
    }
}

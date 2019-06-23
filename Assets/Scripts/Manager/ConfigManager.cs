using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public enum ConfigKey
{
    equipType,
    equipName,
    equipEnum,
    goodsType,
    cardType,
    goods,
    Recommend,
    gamePlayName,
    haerbin,
    mudanjiang,
    happyRoom,
    hupaiType,
    prize,
    nPos,
    poolType,
    soundType,
    canPaiAnmiType,
    PutState,
    LoginView,
    GamehallView,
    GuoType,
    GangType,
    lastHuType,
    gameState,
    roomState,
    nErrorCode,
    aiInfo,
    rookieAward,
    mail,
    mailContent,
    cacheURL
}

//  读取配置类
public class ConfigManager
{
    private static ConfigManager _instance;
    public static ConfigManager Instance { get { return _instance; } }
    private JsonData mConfigData;

    public Dictionary<ConfigKey, JsonData> ConfigData = new Dictionary<ConfigKey, JsonData>();

    private ConfigManager()
    {
        var config = GameAsset.Instance.LoadJson("config");
        mConfigData = JsonMapper.ToObject(config);
        InitData();
    }

    public static void Init()
    {
        if (_instance == null)
        {
            _instance = new ConfigManager();
        }
    }

    private void InitData()
    {
        foreach (var val in mConfigData.Keys)
        {
            //DebugUtils.Log("配置类型" + val);
            ConfigKey configKey = (ConfigKey)Enum.Parse(typeof(ConfigKey), val);
            ConfigData.Add(configKey, mConfigData[val]);
        }
    }

    public static void Dispose()
    {
        if (_instance == null) return;
        Instance.mConfigData.Clear();
        Instance.mConfigData = null;

        Instance.ConfigData.Clear();
        Instance.ConfigData = null;
    }
}

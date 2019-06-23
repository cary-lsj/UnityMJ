using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameAsset
{
    private static GameAsset _instance;

    public static GameAsset Instance
    {
        get
        {
			if(_instance == null)
			{
				_instance = new GameAsset();
			}
            return _instance;
        }
    }

    public const string PATH_DATA = "Assets/GameData/";
    public const string PATH_AUDIO = "Audio/";
    public const string PATH_UI = "UI/";

    public const string EXTENSION_AUDIOCLIP = ".mp3";
    public const string EXTENSION_PREFAB = ".prefab";
    public const string EXTENSION_TEXTURE = ".png";
    public const string EXTENSION_JSON = ".json";

    public const string AUDIO_SYSTEM = "System/";
    public const string AUDIO_MANSOUND = "ManSound/";
    public const string AUDIO_WOMANSOUND = "WomanSound/";

    public string LoadJson(string jsonName)
    {
        var assetPath = jsonName + EXTENSION_JSON;
        object asset = Instance.Load<TextAsset>(assetPath);
        if (asset == null)
        {
            throw new Exception("加载不到json数据" + assetPath);
        }
        return ((TextAsset)asset).text;
    }

    public GameObject LoadPrefab(string pathName)
    {
        GameObject gameObject = Instance.Load<GameObject>(PATH_UI + "Prefab/" + pathName + EXTENSION_PREFAB);
        if (gameObject == null)
        {
            DebugUtils.LogError(string.Format("找不到预设{0}！", pathName));
        }
        return GameObject.Instantiate(gameObject);
    }

    public AudioClip LoadAudio(string pathName)
    {
        AudioClip audioClip = Instance.Load<AudioClip>(PATH_AUDIO + pathName + EXTENSION_AUDIOCLIP);
        if (audioClip == null)
        {
            DebugUtils.LogError(string.Format("找不到音乐{0}!", pathName));
        }
        return audioClip;
    }

    //  AssetDataBase加载
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        string assetPath = PATH_DATA + path;
        return Instance.LoadAsset<T>(assetPath);
    }

    public T LoadAsset<T>(string path) where T : UnityEngine.Object
    {
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
    }

    public static void Dispose()
    {
        if (null != _instance)
        {

        }
        _instance = null;
    }
}

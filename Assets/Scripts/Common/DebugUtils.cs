using UnityEngine;
using System.Text;
using System;

public class DebugUtils
{
    public static void Log(object _log)
    {
        Debug.Log("[GAME]" + _log);
    }

    public static void LogWarning(object _log)
    {
        Debug.LogWarning("[GAMEWARNING]" + _log);
    }

    public static void LogError(object _log)
    {
        Debug.LogError("[GAMEERROR]" + _log);
    }
}


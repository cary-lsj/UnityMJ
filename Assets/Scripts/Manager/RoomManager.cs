using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager
{
    private static RoomManager _instance;
    public static RoomManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public static void Init()
    {
        _instance = new RoomManager();
    }

    public static void Dispose()
    { }
}

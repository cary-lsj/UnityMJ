using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameUI
{
    private static GameUI _instance;

    public static GameUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameUI();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    private Canvas mCanvas;
    private Canvas mTopCanvas;

    public Canvas UICanvas { get { return mCanvas; } }
    public Canvas UITopCanvas { get { return mTopCanvas; } }
    public GameObject GOCanvas { get { return mCanvas.gameObject; } }
    public GameObject GoTopCanvas { get { return mTopCanvas.gameObject; } }

    private List<Widget> mPanels = new List<Widget>();

    readonly string mGroupName = "CurrentGroup";

    Dictionary<GameState, List<Widget>> mSavePanels = new Dictionary<GameState, List<Widget>>();

    private GameUI()
    {
        GameObject goCanvas = GameObject.Find("Canvas");
        mCanvas = goCanvas.transform.Find("Panels").GetComponent<Canvas>();
        mTopCanvas = goCanvas.transform.Find("Dialogs").GetComponent<Canvas>();
    }
    
    public T CreatePanel<T>(object param = null, bool isTopCanvas = false) where T : Widget
    {
        return CreatePanel<T>(null, param, isTopCanvas);
    }

    T CreatePanel<T>(GameObject go, object param, bool isTopCanvas) where T : Widget
    {
        Widget widget = System.Activator.CreateInstance(typeof(T)) as Widget;
        widget.Init(go, param);
        InitPanelData(widget, isTopCanvas);

        return widget as T;
    }

    public void SavePanels(GameState state)
    {
        if (mSavePanels.ContainsKey(state))
            return;

        var go = GetChild(GOCanvas, mGroupName);
        go.name = mGroupName + "_" + state.ToString();
        go.SetActive(false);
        go = GetChild(GoTopCanvas, mGroupName);
        go.name = mGroupName + "_" + state.ToString();
        go.SetActive(false);
        mSavePanels.Add(state, mPanels);

        var list = new List<Widget>(mPanels);
        for (var i = list.Count - 1; i >= 0; i--)
        {
            var panel = list[i];
            if (mPanels.Contains(panel))
            {
                panel.Enable(false);
            }
        }
        mPanels = new List<Widget>();
    }

    public void LoadPanels(GameState state, bool enable)
    {
        if (!mSavePanels.ContainsKey(state))
        {
            return;
        }

        Object.DestroyImmediate(GetChild(GOCanvas, mGroupName));
        Object.DestroyImmediate(GetChild(GoTopCanvas, mGroupName));

        string name = mGroupName + "_" + state;
        var go = GetChild(GOCanvas, name);
        go.name = mGroupName;
        go.SetActive(true);

        go = GetChild(GoTopCanvas, name);
        go.name = mGroupName;
        go.SetActive(true);

        mPanels = mSavePanels[state];
        if (enable)
        {
            for (var i = 0; i < mPanels.Count; i++)
            {
                var panel = mPanels[i];
                if (panel.IsShow)
                {
                    panel.Enable(true);
                }
            }
        }

        mSavePanels.Remove(state);
    }

    public static void Update()
    {
        if (_instance != null)
        {
            _instance.TickUpdate();
        }
    }

    public static void LateUpdate()
    {
        if (_instance != null)
        {
            _instance.TickLateUpdate();
        }
    }

    public void DestroyPanels()
    {
        foreach (var panel in mPanels)
        {
            panel.Destroy();
        }
        mPanels.Clear();
    }

    private void TickUpdate()
    {
        for (int i = 0; i < mPanels.Count; i++)
        {
            mPanels[i].Update();
        }
    }

    private void TickLateUpdate()
    {
        for (int i = 0; i < mPanels.Count; i++)
        {
            mPanels[i].LateUpdate();
        }
    }

    private void InitPanelData(Widget handler, bool isTopCanvas)
    {
        GameObject parent = isTopCanvas ? GoTopCanvas : GOCanvas;

        parent = GetChild(parent, mGroupName);
        mPanels.Add(handler);

        handler.gameObject.UISetParent(parent);

        var rectTF = handler.gameObject.transform as RectTransform;
        rectTF.anchorMax = new Vector2(1, 1);
        rectTF.anchorMin = new Vector2(0, 0);
        rectTF.pivot = new Vector2(0.5f, 0.5f);
        rectTF.localPosition = Vector3.zero;
        rectTF.offsetMin = Vector2.zero;
        rectTF.offsetMax = Vector2.zero;
    }

    GameObject GetChild(GameObject parent, string name)
    {
        var tr = parent.transform.Find(name);
        if (tr == null)
        {
            var Panels = new GameObject(name, typeof(RectTransform));
            Panels.UISetParent(parent);
            tr = Panels.transform;
            ((RectTransform)tr).anchorMax = new Vector2(1, 1);
            ((RectTransform)tr).anchorMin = new Vector2(0, 0);
            ((RectTransform)tr).pivot = new Vector2(0.5f, 0.5f);
            tr.localPosition = Vector3.zero;
            ((RectTransform)tr).offsetMin = Vector2.zero;
            ((RectTransform)tr).offsetMax = Vector2.zero;
        }
        return tr.gameObject;
    }

    public static void Clear(bool clearAll = false)
    {
        if (_instance != null)
        {
            _instance.ClearPanels(clearAll);
        }
    }

    private void ClearPanels(bool bclearAll = false)
    {
        CloseAll(bclearAll);
    }

    void CloseAll(bool bClear = true)
    {
        while (mPanels.Count > 0)
        {
            ClosePanel(mPanels[0]);
        }
    }

    void ClosePanel(Widget widget)
    {
        if (mPanels.Contains(widget))
        {
            mPanels.Remove(widget);
            widget.Close();
        }
        else
        {
            foreach (var panels in mSavePanels.Values)
            {
                if (panels.Contains(widget))
                {
                    panels.Remove(widget);
                    widget.Close();
                }
            }
        }
    }

    public void RemovePanel(Type t)
    {
        Widget widget = mPanels.FindLast(v => v.GetType() == t);
        if (widget != null)
        {
            mPanels.Remove(widget);
            return;
        }
    }
}
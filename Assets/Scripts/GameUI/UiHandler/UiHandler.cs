using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;
using UnityEngine;
using System.Collections;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class UiInitAttribute : Attribute
{
    public UiInitAttribute(String Description_in)
    {
        this.description = Description_in;
    }

    protected String description;
    public String Description
    {
        get
        {
            return this.description;
        }
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class UiNameAttribute : Attribute
{
    public UiNameAttribute(String Description_in = null)
    {
        this.description = Description_in;
    }

    protected String description;
    public String Description
    {
        get
        {
            return this.description;
        }
    }
}

public class Widget : UiBase
{
   
}

public class Listitem : UiBase
{

}

public class UiBase : GameBase
{
    public GameObject gameObject
    {
        get { return mUiPrefab; }
    }

    public RectTransform transform
    {
        get { return mUiPrefab.transform as RectTransform; }
    }

    public bool active
    {
        set
        {
            if (gameObject != null)
            {
                gameObject.SetActive(value);
            }
        }
        get
        {
            if (gameObject == null)
                return false;
            return gameObject.activeSelf;
        }
    }

    public string Path { get; set; }

    private bool mLoad;
    protected object mParm;

    private GameObject mUiPrefab;
    private UiBase mParent;
    private List<UiBase> mChilds = new List<UiBase>();
    Dictionary<string, object> Binds = new Dictionary<string, object>();

    public void SetShow(bool bShow)
    {
        if (mUiPrefab == null)
            return;
        if (mUiPrefab.activeSelf == false && bShow)
            OnEnable();
        else if (mUiPrefab.activeSelf && bShow == false)
            OnDisable();
        mUiPrefab.SetActive(bShow);
    }

    public void Enable(bool b)
    {
        if (b)
            OnEnable();
        else
            OnDisable();
    }

    public bool IsShow
    {
        get { return mUiPrefab == null ? false : mUiPrefab.activeSelf; }
    }

    public void Init(GameObject go, object param)
    {
        mUiPrefab = go;
        mLoad = false;

        if (null == go)
        {
            InitAttr();
            mUiPrefab = GameAsset.Instance.LoadPrefab(Path);
            mUiPrefab.SetActive(true);
            mLoad = true;
        }
        if (mUiPrefab != null)
        {
            InitData();
            mParm = param;
            OnStart();
        }
    }

    void InitAttr()
    {
        Path = GetType().Name;
        object[] classAttr = GetType().GetCustomAttributes(typeof(UiInitAttribute), true);
        if (classAttr.Length > 0)
            Path = (classAttr[0] as UiInitAttribute).Description;

        classAttr = GetType().GetCustomAttributes(typeof(UiNameAttribute), true);
        if (classAttr.Length > 0)
            Path = (classAttr[0] as UiNameAttribute).Description;
    }

    void InitData()
    {
        Type t = GetType();
        do
        {
            FieldInfo[] fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++)
            {
                var fieldAttr = fields[i].GetCustomAttributes(typeof(UiNameAttribute), true);
                var fieldAttrInit = fields[i].GetCustomAttributes(typeof(UiInitAttribute), true);
                if (fieldAttr.Length > 0)
                {
                    UiNameAttribute attr = fieldAttr[0] as UiNameAttribute;
                    string bindkey = string.IsNullOrEmpty(attr.Description) ? fields[i].Name : attr.Description;
                    object widget = FindBindingWidget(bindkey, fields[i].FieldType);
                    if (widget != null)
                    {
                        fields[i].SetValue(this, widget);
                        if (!Binds.ContainsKey(bindkey))
                            Binds.Add(bindkey, widget);
                    }
                    else
                    {
                        if (fieldAttrInit.Length == 0)
                        {
                            Debug.LogError(string.Format("Class = {0}", t.Name));
                            Debug.LogError(string.Format("[{0}]没有绑定！Type = {1}! Name = {2}", attr.Description, fields[i].FieldType.Name, fields[i].Name));
                            Debug.LogError(string.Format(GetPath(transform, "")));
                        }
                        UiInitAttribute attrInit = fieldAttrInit[0] as UiInitAttribute;
                        if (fields[i].FieldType.BaseType == typeof(Array))
                            fields[i].SetValue(this, FindUiWidgets(attrInit.Description, fields[i].FieldType));
                        else
                            fields[i].SetValue(this, FindUiWidget(attrInit.Description, fields[i].FieldType));
                    }
                }
                else if (fieldAttrInit.Length > 0)
                {
                    UiInitAttribute attrInit = fieldAttrInit[0] as UiInitAttribute;
                    if (fields[i].FieldType.BaseType == typeof(Array))
                        fields[i].SetValue(this, FindUiWidgets(attrInit.Description, fields[i].FieldType));
                    else
                        fields[i].SetValue(this, FindUiWidget(attrInit.Description, fields[i].FieldType));
                }
            }

            t = t.BaseType;
        }
        while (t != typeof(UiBase) && t != typeof(Widget));
    }

    string GetPath(Transform tf, string path)
    {
        if (tf.parent == null)
            return path;
        path = path.Insert(0, tf.name + "/");
        return GetPath(tf.parent, path);
    }

    object FindUiWidgets(string Path, Type t)
    {
        Transform tr = FindTransform(mUiPrefab, Path);
        if (tr == null || tr.childCount == 0) return null;
        Array array = Array.CreateInstance(t.GetElementType(), tr.childCount);
        Type type = t.GetElementType();
        Transform[] childs = new Transform[tr.childCount];
        for (int i = 0; i < tr.childCount; i++)
        {
            childs[i] = tr.GetChild(i);
        }

        for (int i = 0; i < childs.Length; i++)
        {
            object o = FindUiWidget(string.IsNullOrEmpty(Path) ? childs[i].name : Path + "/" + childs[i].name, type);
            array.SetValue(o, i);
        }
        return array;
    }

    object FindUiWidget(string Path, Type t)
    {
        object o = null;
        if (t == typeof(GameObject))
        {
            o = FindGameObject(mUiPrefab, Path);
        }
        else if (t == typeof(Transform))
        {
            o = FindTransform(mUiPrefab, Path);
        }
        //else if (t.BaseType == typeof(ListitemHandler))
        //{
        //    return CreateListitemHandler(t, FindGameObject(mUiPrefab, Path));
        //}
        else
        {
            o = FindComponent(mUiPrefab, Path, t);
        }
        if (o == null)
        {
            DebugUtils.LogWarning("path no find !! path = " + Path + "type = " + t.Name);
        }
        return o;
    }

    object FindBindingWidget(string binding, Type t)
    {
        object o = null;

        var binds = mUiPrefab.GetComponentsInChildren<UIBind>(true);
        var bind = binds.FirstOrDefault(v => v.Value == binding);
        if (bind != null)
        {
            o = bind.GetBindWidget(this, t);
            bind.Type = o;
        }
        return o;
    }

    static GameObject FindGameObject(GameObject go, string path)
    {
        Transform tChild = go.transform.Find(path);
        if (tChild)
            return tChild.gameObject;
        else
            return null;
    }

    static Transform FindTransform(GameObject go, string path)
    {
        Transform tChild = go.transform.Find(path);
        if (tChild)
            return tChild;
        return null;
    }

    static Component FindComponent(GameObject go, string path, Type t)
    {
        if (go == null) return null;

        if (string.IsNullOrEmpty(path))
            return go.GetComponent(t);
        else
        {
            Transform tChild = go.transform.Find(path);
            if (tChild)
                return tChild.gameObject.GetComponent(t);
        }
        return null;
    }

    public void Close()
    {
        OnClose();
        if (mParent != null)
        {
            if (mLoad != false)
            {
                Object.Destroy(gameObject);
            }
            mParent.mChilds.Remove(this);
        }
        else
        {
            GameUI.Instance.RemovePanel(GetType());
            Object.Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        if (mParent != null)
        {
            if (mLoad != false)
            {
                Object.Destroy(gameObject);
            }
            mParent.mChilds.Remove(this);
        }
        else
        {
            Object.Destroy(gameObject);
        }

        for (int i = 0; i < mChilds.Count; i++)
        {
            mChilds[i].Destroy();
        }
        Destroy(this);
    }

    public void Update()
    {
        if (!active)
            return;

        OnUpdate();
        for (int i = 0; i < mChilds.Count; i++)
        {
            if (mChilds[i].active)
                mChilds[i].Update();
        }
    }

    public void LateUpdate()
    {
        OnLateUpdate();
        for (int i = 0; i < mChilds.Count; i++)
        {
            mChilds[i].LateUpdate();
        }
    }

    protected virtual void OnStart()
    { }

    protected virtual void OnUpdate()
    { }

    protected virtual void OnLateUpdate()
    { }

    protected virtual void OnEnable()
    { }

    protected virtual void OnDisable()
    { }

    protected virtual void OnClose()
    { }
}

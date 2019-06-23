using System;
using UnityEngine;

public class UIBind : MonoBehaviour
{
    public object Type;
    public string Value;

    public Type GetWidgetClass()
    {
        Type type = null;
        var bindClass = GetComponentInParent<UIBindClass>();
        if (bindClass == null)
            return type;

        if (bindClass.name == gameObject.name && bindClass.transform.parent != null)
        {
            var temp = bindClass.transform.parent.GetComponentInParent<UIBindClass>();
            if (temp != null) bindClass = temp;
        }

        if (!String.IsNullOrEmpty(bindClass.ClassName))
            type = System.Type.GetType(bindClass.ClassName);
        if (type == null)
            type = System.Type.GetType(bindClass.name);
        return type;
    }

    public object GetBindWidget(UiBase uibase, Type t)
    {
        if (t.BaseType == typeof(Array))
            return GetBindComponents(uibase, t);

        return GetBindComponent(uibase, t);
    }

    public object GetBindComponent(UiBase uibase, Type t, GameObject target = null)
    {
        GameObject go = target == null ? gameObject : target;
        if (t == typeof(GameObject))
            return go;
        //if (typeof(ListitemHandler).IsAssignableFrom(t))
        //    return handler.CreateListitemHandler(t, go);
        return go.GetComponent(t);
    }

    public object GetBindComponents(UiBase uibase, Type t)
    {
        Type type = t.GetElementType();
        Array array = Array.CreateInstance(type, transform.childCount);
        Transform[] childs = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            childs[i] = transform.GetChild(i);

        for (int i = 0; i < childs.Length; ++i)
        {
            object o = GetBindComponent(uibase, type, childs[i].gameObject);
            array.SetValue(o, i);
        }

        return array;
    }
}

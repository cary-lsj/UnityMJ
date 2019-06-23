using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIBind))]
public class UIBindEditor : Editor
{
    UIBind Bind;
    SerializedProperty SerializedValue;
    List<string> NodeOptions = new List<string>();

    private void OnEnable()
    {
        Bind = target as UIBind;
        SerializedValue = serializedObject.FindProperty("Value");

        Type type = Bind.GetWidgetClass();
        if (type == null)
            return;

        NodeOptions.Clear();

        var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        for (int i = 0; i < fields.Length; ++i)
        {
            var filed = fields[i];
            object[] fieldAttr = filed.GetCustomAttributes(typeof(UiNameAttribute), true);
            if (fieldAttr.Length == 0)
                continue;

            UiNameAttribute attr = fieldAttr[0] as UiNameAttribute;
            string bindkey = string.IsNullOrEmpty(attr.Description) ? filed.Name : attr.Description;

            NodeOptions.Add(bindkey);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUILayout.Space(10);
        List<string> options = new List<string>();

        options.AddRange(NodeOptions);

        int index = options.IndexOf(SerializedValue.stringValue);
        int selectIndex = EditorGUILayout.Popup("值绑定====", index, options.ToArray(), GUILayout.ExpandWidth(true));
        if (index != selectIndex)
            Bind.Value = options[selectIndex];

        GUILayout.Space(10);
        if (GUI.changed)
            EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
    }
}

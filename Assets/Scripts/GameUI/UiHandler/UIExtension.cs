using UnityEngine;
using UnityEngine.UI;

public static class UIExtension
{
    public static void UISetParent(this GameObject go, GameObject parent)
    {
        Transform tf = go.transform;
        tf.SetParent(parent.transform);
        tf.localPosition = Vector3.zero;
        tf.localRotation = Quaternion.identity;
        tf.localScale = Vector3.one;
    }

}

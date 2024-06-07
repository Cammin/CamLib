using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsGameObject
    {
        public static GameObject CreateChildGameObject(this GameObject parent, string name = "New GameObject")
        {
            GameObject child = new GameObject(name);
            child.transform.SetParent(parent.transform);
            child.transform.localPosition = Vector3.zero;
            return child;
        }
    }
}

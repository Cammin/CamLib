using UnityEngine;

namespace CamLib
{
    public static class GameObjectExtensions
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

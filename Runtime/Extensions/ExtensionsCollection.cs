using System.Collections.Generic;
using UnityEngine;

namespace CamLib
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> list)
        {
            return list == null || list.Count == 0;
        }
        
        public static bool IsIndexValid<T>(this ICollection<T> list, int index)
        {
            if (list == null) return false;
            if (list.Count <= index) return false;
            if (index < 0) return false;
            return true;
        }
        
        public static T Pop<T>(this IList<T> list)
        {
            T thing = list[^1];
            list.RemoveAt(list.Count-1);
            return thing;
        }
        
        public static T[] Shuffle<T> (this T[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                int swap = Random.Range(0, list.Length);
                (list[swap], list[i]) = (list[i], list[swap]);
            }
            return list;
        }
        
        public static IList<T> Shuffle<T> (this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int swap = Random.Range(0, list.Count);
                (list[swap], list[i]) = (list[i], list[swap]);
            }
            return list;
        }
        
        public static T GetRandomElement<T>(this T[] array)
        {
            if (array.IsNullOrEmpty())
            {
                return default;
            }

            int random = Random.Range(0, array.Length);
            return array[random];
        }
        
        public static T GetRandomElement<T>(this IList<T> list)
        {
            if (list.IsNullOrEmpty())
            {
                return default;
            }

            int random = Random.Range(0, list.Count);
            return list[random];
        }
    }
}
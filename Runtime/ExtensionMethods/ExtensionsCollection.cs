using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> list)
        {
            return list == null || list.Count == 0;
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
        
        public static T GetRandomElement<T>(this T[] collection)
        {
            if (collection.IsNullOrEmpty())
            {
                return default;
            }

            int random = Random.Range(0, collection.Length);
            return collection.ElementAt(random);
        }
        public static T GetRandomElement<T>(this IList<T> collection)
        {
            if (collection.IsNullOrEmpty())
            {
                return default;
            }

            int random = Random.Range(0, collection.Count);
            return collection.ElementAt(random);
        }
    }
}
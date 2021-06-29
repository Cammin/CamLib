using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CamLib
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
        
        public static T GetRandomElement<T>(this IEnumerable<T> collection)
        {
            collection = collection as T[] ?? collection.ToArray();

            if (collection.IsNullOrEmpty())
            {
                return default;
            }

            int random = Random.Range(0, collection.Count());
            return collection.ElementAt(random);
        }
    }
}
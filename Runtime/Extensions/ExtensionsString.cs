using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsString
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        
        public static string ToStringSpacedCommas<T>(this ICollection<T> list)
        {
            IEnumerable<string> strings = list.Select(p => p.ToString());
            string joined = string.Join(", ", strings);
            joined = $"({joined})";
            return joined;
        }
    }
}
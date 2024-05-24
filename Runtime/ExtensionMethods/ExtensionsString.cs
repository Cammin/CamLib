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
    }
}
using UnityEngine;

namespace CamLib
{
    // Use LOG_RELEASE_DISABLE in player settings to make this work
    internal static class DebugLogEnabled
    {
#if LOG_RELEASE_DISABLE
        [RuntimeInitializeOnLoadMethod]
        private static void AllowDebugging()
        {
            Debug.unityLogger.logEnabled = Debug.isDebugBuild;
        }
#endif
    }
}

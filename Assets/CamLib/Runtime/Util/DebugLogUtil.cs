using UnityEngine;

namespace CamLib
{
    // Use LOG_RELEASE_DISABLE in player settings to make this work
    public static class DebugLogUtil
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

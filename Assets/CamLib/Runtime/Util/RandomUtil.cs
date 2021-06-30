using UnityEngine;

namespace CamLib.Util
{
    public static class RandomTool
    {
        public static bool CoinFlip() => Random.value >= 0.5f;
    }
}

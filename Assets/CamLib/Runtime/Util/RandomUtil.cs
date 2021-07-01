using UnityEngine;

namespace CamLib
{
    public static class RandomTool
    {
        public static bool CoinFlip() => Random.value >= 0.5f;
    }
}

using UnityEngine;

namespace CamLib
{
    public static class RandomUtil
    {
        public static bool CoinFlip() => Random.value >= 0.5f;
    }
}

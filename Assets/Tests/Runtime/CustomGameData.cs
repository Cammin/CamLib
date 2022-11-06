using System;
using CamLib;

namespace Tests
{
    [Serializable]
    public class CustomGameData : GameData
    {
        public bool WeHaveIt;

        public override void OnConstruct()
        {
            WeHaveIt = true;
        }
    }
}
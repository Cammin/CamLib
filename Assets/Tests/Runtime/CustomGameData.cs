using System;
using CamLib;

namespace Tests
{
    [Serializable]
    public class CustomGameData : GameDataBase
    {
        public bool WeHaveIt;

        public override void OnConstruct()
        {
            WeHaveIt = true;
        }
    }
}
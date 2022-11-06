using System;
using System.Collections.Generic;
using UnityEngine;

namespace CamLib
{
    [Serializable]
    //to make this data able to be extended from for any project,this must be inherited from as a custom class now
    public class GameData
    {
        public long LastUpdated;

        /// <summary>
        /// the values defined in this constructor will be the default values
        /// the game starts with when there's no data to load
        /// </summary>
        public virtual void OnConstruct() {} 
    }

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

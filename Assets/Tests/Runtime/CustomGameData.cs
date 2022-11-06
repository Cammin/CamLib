using System;
using CamLib;
using UnityEngine;

namespace Tests
{
    [Serializable]
    public class CustomGameData : GameData
    {
        public bool WeHaveIt;
        public Vector2 ObjPos;

        public override void OnConstruct()
        {
            WeHaveIt = true;
            ObjPos = new Vector2();
        }
    }
}
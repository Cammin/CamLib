using System.IO;
using UnityEngine;

namespace CamLib
{
    internal static class PlayerDataSave
    {
        public static void SaveData<T>(T playerData, bool prettyPrint = false)
        {
            string path = PlayerData.DataPath;
        
            string json = JsonUtility.ToJson(playerData, prettyPrint);
            File.WriteAllText(path, json);
        }
    }
}


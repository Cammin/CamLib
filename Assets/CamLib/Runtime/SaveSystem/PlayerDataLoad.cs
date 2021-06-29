using System;
using System.IO;
using UnityEngine;

namespace CamLib
{
    internal static class PlayerDataLoad
    {
        public static T LoadData<T>()
        {
            string path = PlayerData.DataPath;
            
            if (!File.Exists(PlayerData.DataPath))
            {
                return default;
            }
        
            string json = File.ReadAllText(path);
            try
            {
                T data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch(Exception e)
            {
                Debug.LogError($"PlayerData failed to load json. Corrupted?\n{e}");
            }
            return default;
        }
    }
}


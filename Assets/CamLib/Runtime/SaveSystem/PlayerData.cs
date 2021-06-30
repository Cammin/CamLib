using UnityEngine;

namespace CamLib
{
    public static class PlayerData
    {
        private const string PLAYER_DATA_FILE_NAME = "playerdata.json";
        internal static string DataPath => $"{Application.persistentDataPath}/{PLAYER_DATA_FILE_NAME}";

        public static T Load<T>() => PlayerDataLoad.LoadData<T>();
        public static void Save<T>(T data) => PlayerDataSave.SaveData(data);
    }
}

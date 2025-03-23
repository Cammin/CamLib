namespace CamLib
{
    internal static class DataPersistenceEditorPrefs
    {
        private const string PrefKeyDisableDataPersistence = "DataPersistenceManagerEditor.PrefKeyDisableDataPersistence";
        private const string PrefKeyInitializeDataIfNull = "DataPersistenceManagerEditor.PrefKeyInitializeDataIfNull";
        private const string PrefKeyLoadBeforeFirstSceneLoad = "DataPersistenceManagerEditor.PrefKeyLoadBeforeFirstSceneLoad";
        private const string PrefKeyTestSelectedProfileId = "DataPersistenceManagerEditor.PrefKeyTestSelectedProfileId";

        internal static bool DisableDataPersistence
        {
#if UNITY_EDITOR
            get => UnityEditor.EditorPrefs.GetBool(PrefKeyDisableDataPersistence, false);
            set => UnityEditor.EditorPrefs.SetBool(PrefKeyDisableDataPersistence, value);
#else
            get => false;
#endif
        }
        
        internal static bool InitializeDataIfNull
        {
#if UNITY_EDITOR
            get => UnityEditor.EditorPrefs.GetBool(PrefKeyInitializeDataIfNull, false);
            set => UnityEditor.EditorPrefs.SetBool(PrefKeyInitializeDataIfNull, value);
#else
            get => false;
#endif
        }

        internal static bool LoadBeforeFirstSceneLoad
        {
#if UNITY_EDITOR
            get => UnityEditor.EditorPrefs.GetBool(PrefKeyLoadBeforeFirstSceneLoad, false);
            set => UnityEditor.EditorPrefs.SetBool(PrefKeyLoadBeforeFirstSceneLoad, value);
#else
            get => false;
#endif
        }
         
        internal static string TestSelectedProfileId
        {
#if UNITY_EDITOR
            get => UnityEditor.EditorPrefs.GetString(PrefKeyTestSelectedProfileId, string.Empty);
            set => UnityEditor.EditorPrefs.SetString(PrefKeyTestSelectedProfileId, value);
#else
            get => string.Empty;
#endif
        }
    }
}
using CamLib;
using CamLib.Editor;
using UnityEditor;

namespace Tests.Editor
{
    [CustomEditor(typeof(TestPersistenceManager))]
    public class CustomDataPersistenceManagerEditor : DataPersistenceManagerEditor<CustomGameData, CustomDataWindow>
    {
    }
}
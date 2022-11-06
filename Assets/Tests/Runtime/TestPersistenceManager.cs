using CamLib;
using UnityEngine;

namespace Tests
{
    public class TestPersistenceManager : DataPersistenceManager<CustomGameData>
    {
        [ContextMenu("Save")]
        public void TestSave()
        {
            SaveGame();
        }
        [ContextMenu("Load")]
        public void TestLoad()
        {
            LoadGame();
        }
        [ContextMenu("NewGame")]
        public void TestNewGame()
        {
            NewGame();
        }
    }
}
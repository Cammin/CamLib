using CamLib;
using UnityEngine;

namespace Tests
{
    public class TestPersistenceObject : MonoBehaviour, IDataPersistence<CustomGameData>
    {
        public void LoadData(CustomGameData data)
        {
            transform.position = data.ObjPos;
        }

        public void SaveData(CustomGameData data)
        {
            data.ObjPos = transform.position;
        }
    }
}

using System;
using UnityEngine;

namespace CamLib
{

    public abstract class SaveDataMockAsset : ScriptableObject
    {
        
    }
    
    //public interface 
    
    //[Serializable, CreateAssetMenu(fileName = "SaveDataMockAsset", menuName = "CamLib/SaveData MockAsset")]
    public abstract class SaveDataMockAsset<T> : ScriptableObject
    {
        [SerializeField] private T _save;
        public T Data => _save;
    }
}
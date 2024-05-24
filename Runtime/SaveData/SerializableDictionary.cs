using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CamLib
{
    [System.Serializable]
    public class SerializableDictionary<TValue> : Dictionary<string, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<string> keys = new List<string>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        // save the dictionary to lists
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<string, TValue> pair in this) 
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // load the dictionary from lists
        public void OnAfterDeserialize()
        {
            this.Clear();
            
            //resize the array to ensure it's always the same size as 
            if (keys.Count > values.Count)
            {
                for (int i = values.Count; i < keys.Count; i++)
                {
                    values.Add(default);
                }
            }
            if (values.Count > keys.Count)
            {
                for (int i = keys.Count; i < values.Count; i++)
                {
                    values.RemoveAt(i);
                }
            }
            
            if (keys.Count != values.Count) 
            {
                Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys ("
                               + keys.Count + ") does not match the number of values (" + values.Count 
                               + ") which indicates that something went wrong");
                return;
            }

            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                if (ContainsKey(key))
                {
                    key = Path.GetRandomFileName();
                }
                this.Add(key, values[i]);
            }
        }
    }
}

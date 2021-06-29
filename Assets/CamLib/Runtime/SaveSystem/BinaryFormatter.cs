// may implement this at a later date to allow encrypted save data

/*
    public static class BinaryFormatter
    {
        /*private static string Path<T>() => $"{Application.persistentDataPath}/{FileName<T>()}.save";
    
        private static string FileName<T>()
        {
            return typeof(T).Name;
        }
    
        public static void Save<T>(T objectToSave) where T : class
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(Path<T>());
            formatter.Serialize(file, objectToSave);
            file.Close();
    
            Debug.Log($"Game Saved at {Path<T>()}");
        }
    
        public static T Load<T>() where T : class
        {
            if (File.Exists(Path<T>()))
            {
                BinaryFormatter formatter = new BinaryFormatter();
    
                FileStream file = File.Open(Path<T>(), FileMode.Open);
                file.Position = 0;
                T save = (T)formatter.Deserialize(file);
    
                file.Close();
    
                Debug.Log($"{FileName<T>()} Loaded");
                return save;
            }
            else
            {
                Debug.Log($"No save was found to load for {FileName<T>()}!");
            }
            return null;
        }
    
        public static void Delete<T>()
        {
            if (File.Exists(Path<T>()))
            {
                File.Delete(Path<T>());
    
                Debug.Log($"{FileName<T>()} Deleted");
            }
            else
            {
                Debug.Log($"No save was found to delete for {FileName<T>()}!");
            }
    }
        }*/




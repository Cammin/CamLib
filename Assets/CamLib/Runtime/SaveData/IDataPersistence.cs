namespace CamLib
{
    public interface IDataPersistence<in T> where T : GameData
    {
        void LoadData(T data);

        // The 'ref' keyword was removed from here as it is not needed.
        // In C#, non-primitive types are automatically passed by reference.
        void SaveData(T data);
    }
}

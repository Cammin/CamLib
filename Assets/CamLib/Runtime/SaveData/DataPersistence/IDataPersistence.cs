namespace CamLib
{
    public interface IDataPersistence
    {
        void LoadData(GameData data);

        // The 'ref' keyword was removed from here as it is not needed.
        // In C#, non-primitive types are automatically passed by reference.
        void SaveData(GameData data);
    }
}

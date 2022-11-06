namespace CamLib
{
    public interface IDataPersistence<in T> where T : GameData
    {
        /// <summary>
        /// When we're reading the data to retain previous session
        /// </summary>
        void LoadData(T data);

        /// <summary>
        /// When we're writing our data to store later
        /// </summary>
        void SaveData(T data);
    }
}

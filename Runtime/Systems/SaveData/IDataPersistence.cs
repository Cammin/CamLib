namespace CamLib
{
    /// <summary>
    /// Make any instances/entities implement this interface to be able to save and load data. For example, a coin in a level that remembers if it was collected.
    /// </summary>
    public interface IDataPersistence<in T> where T : GameData
    {
        /// <summary>
        /// Called at the start of the scene when this object was found.
        /// When we're reading the data to retain previous session. It's up to you how you want to use a unique identifier, or not.
        /// </summary>
        void LoadData(T data);

        /// <summary>
        /// When we're writing our data to store later. i.e. Make a coin memorize it was collected.
        /// </summary>
        void SaveData(T data);
    }
}

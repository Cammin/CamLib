namespace CamLib
{
    /// <summary>
    /// Derive from this class and serve as your game's master save data class.
    /// </summary>
    public abstract class GameData
    {
        public long LastUpdated;

        /// <summary>
        /// the values defined in this constructor will be the default values
        /// the game starts with when there's no data to load
        /// </summary>
        public virtual void OnConstruct() {} 
    }
}

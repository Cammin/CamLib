namespace CamLib
{
    //to make this data able to be extended from for any project, this must be inherited from as a custom class now
    public abstract class GameDataBase
    {
        public long LastUpdated;

        /// <summary>
        /// the values defined in this constructor will be the default values
        /// the game starts with when there's no data to load
        /// </summary>
        public virtual void OnConstruct() {} 
    }
}

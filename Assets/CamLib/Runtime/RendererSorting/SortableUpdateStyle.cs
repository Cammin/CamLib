namespace CamLib
{
    internal enum SortableUpdateStyle
    {
        /// <summary>
        /// An easy option to update the order by the manager every given time interval. 
        /// </summary>
        UpdatedByManager,
        
        /// <summary>
        /// Would set the order once, and never again. Use this if the object is not expected to move.
        /// </summary>
        OnlyOnce,
        
        /// <summary>
        /// This object would be responsible for it's own updating of order. Best to try updating upon changing y position.
        /// </summary>
        SelfManaged,
    }
}
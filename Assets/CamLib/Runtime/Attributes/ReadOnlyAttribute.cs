using UnityEngine;

namespace CamLib
{
    public enum ReadOnlyStatus
    {
        Both,
        Editor,
        PlayMode
    }
    /// <summary>
    /// Use to define that a field is readonly. Additional paramete
    /// </summary>
    public class ReadOnlyAttribute : PropertyAttribute
    {
        public readonly ReadOnlyStatus Status = ReadOnlyStatus.Both;
        public ReadOnlyAttribute()
        {
            
        }
        public ReadOnlyAttribute(ReadOnlyStatus status)
        {
            Status = status;
        }
    }
}
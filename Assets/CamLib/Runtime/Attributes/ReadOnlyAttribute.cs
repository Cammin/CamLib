using UnityEngine;

namespace CamLib
{
    public enum ReadOnlyStatus
    {
        Both,
        Editor,
        PlayMode
    }
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
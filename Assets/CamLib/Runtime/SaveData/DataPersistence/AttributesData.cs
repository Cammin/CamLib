namespace CamLib
{
    [System.Serializable]
    public class AttributesData
    {
        public int vitality;
        public int strength;
        public int intellect;
        public int endurance;

        public AttributesData() 
        {
            this.vitality = 1;
            this.strength = 1;
            this.intellect = 1;
            this.endurance = 1;
        }
    }
}

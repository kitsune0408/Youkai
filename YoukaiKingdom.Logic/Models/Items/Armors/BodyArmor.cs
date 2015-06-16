namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    public class BodyArmor : Armor
    {
        private  const int DefaultDefense = 50;
        private const int DefaultLevel = 1;

        public BodyArmor(string name, int level, int defensePoints)
            : base(name, level, defensePoints)
        {
        }
        public BodyArmor(string name)
            : base(name, DefaultLevel, DefaultDefense)
        {
        }
        
        
        
    }
}

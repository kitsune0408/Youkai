namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    public class BodyArmor : Armor
    {
        private const int DefaultDefense = 50;
        private const int DefaultLevel = 1;

        public BodyArmor(int id, string name, int level, int defensePoints)
            : base(id, name, level, defensePoints)
        {
        }
        public BodyArmor(int id, string name)
            : base(id, name, DefaultLevel, DefaultDefense)
        {
        }
    }
}

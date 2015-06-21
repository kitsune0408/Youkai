namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    public class Gloves : Armor
    {
        private const int DefaultDefense = 70;
        private const int DefaultLevel = 1;

        public Gloves(int id, string name, int level, int defensePoints, bool generateBonusAttributes = true) : base(id, name, level, defensePoints, generateBonusAttributes) { }

        public Gloves(int id, string name, bool generateBonusAttributes = true) : base(id, name, DefaultLevel, DefaultDefense, generateBonusAttributes) { }
    }
}

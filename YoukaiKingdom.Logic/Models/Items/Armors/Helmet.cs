namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    public class Helmet : Armor
    {
        private const int DefaultDefense = 90;
        private const int DefaultLevel = 1;

        public Helmet(int id, string name, int level, int defensePoints, bool generateBonusAttributes = true) : base(id, name, level, defensePoints, generateBonusAttributes) { }

        public Helmet(int id, string name, bool generateBonusAttributes = true) : base(id, name, DefaultLevel, DefaultDefense, generateBonusAttributes) { }
    }
}

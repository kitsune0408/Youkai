namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    public class TwoHandedStaff : Weapon
    {
        private const int DefaultAttackPoints = 120;
        private const int DefaultLevel = 1;

        public TwoHandedStaff(int id, string name, int level, int attackPoints, bool generateBonusAttributes = true) : base(id, name, level, attackPoints, generateBonusAttributes) { }

        public TwoHandedStaff(int id, string name, bool generateBonusAttributes = true) : base(id, name, DefaultLevel, DefaultAttackPoints, generateBonusAttributes) { }
    }
}
namespace YoukaiKingdom.Logic.Models.Items.Weapons.TwoHanded
{
    public class TwoHandedStaff : TwoHandedWeapon
    {
        private const int DefaultAttackPoints = 120;
        private const int DefaultLevel = 1;

        public TwoHandedStaff(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true)
            : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }

        public TwoHandedStaff(int id, string name, int attackSpeed, bool generateBonusAttributes = true)
            : this(id, name, DefaultLevel, DefaultAttackPoints, attackSpeed, generateBonusAttributes) { }
    }
}
namespace YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded
{
    public class OneHandedSword : OneHandedWeapon
    {
        private const int DefaultAttackPoints = 70;

        private const int DefaultLevel = 1;

        public OneHandedSword(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true) : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }

        public OneHandedSword(int id, string name, int attackSpeed, bool generateBonusAttributes = true) : this(id, name, DefaultLevel, DefaultAttackPoints, attackSpeed, generateBonusAttributes) { }
    }
}

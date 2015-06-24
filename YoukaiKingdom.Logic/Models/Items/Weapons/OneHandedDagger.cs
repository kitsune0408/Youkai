namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    using YoukaiKingdom.Logic.Interfaces;

    public class OneHandedDagger : Weapon, IOffhand
    {
        private const int DefaultAttackPoints = 40;
        private const int DefaultLevel = 1;

        public OneHandedDagger(int id, string name, int level, int attackPoints, bool generateBonusAttributes = true) : base(id, name, level, attackPoints, generateBonusAttributes) { }

        public OneHandedDagger(int id, string name, bool generateBonusAttributes = true) : base(id, name, DefaultLevel, DefaultAttackPoints, generateBonusAttributes) { }
    }
}
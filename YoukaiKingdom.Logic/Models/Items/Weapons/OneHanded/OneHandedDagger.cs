using System;

namespace YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded
{
    [Serializable]
    public class OneHandedDagger : OneHandedWeapon
    {
        private const int DefaultAttackPoints = 40;

        private const int DefaultLevel = 1;

        private OneHandedDagger() { }

        public OneHandedDagger(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true)
            : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }

        public OneHandedDagger(int id, string name, int attackSpeed, bool generateBonusAttributes = true)
            : this(id, name, DefaultLevel, DefaultAttackPoints, attackSpeed, generateBonusAttributes) { }
    }
}
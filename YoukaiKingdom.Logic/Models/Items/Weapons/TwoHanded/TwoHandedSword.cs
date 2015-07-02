using System;
using System.Xml.Serialization;

namespace YoukaiKingdom.Logic.Models.Items.Weapons.TwoHanded
{
    [Serializable]
    public class TwoHandedSword : TwoHandedWeapon
    {
        private const int DefaultAttackPoints = 120;
        private const int DefaultLevel = 1;

        public TwoHandedSword(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true)
            : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }

        public TwoHandedSword(int id, string name, int attackSpeed, bool generateBonusAttributes = true)
            : this(id, name, DefaultLevel, DefaultAttackPoints, attackSpeed, generateBonusAttributes) { }

        private TwoHandedSword() { }
    }
}

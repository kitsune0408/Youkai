using System;

namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    using YoukaiKingdom.Logic.Interfaces;

    [Serializable]
    public class Shield : Armor, IOffhand
    {
        private const int DefaultDefense = 10;
        private const int DefaultLevel = 1;

        public Shield() {}

        public Shield(int id, string name, int level, int defensePoints, bool generateBonusAttributes = true) : base(id, name, level, defensePoints, generateBonusAttributes) { }

        public Shield(int id, string name, bool generateBonusAttributes = true) : base(id, name, DefaultLevel, DefaultDefense, generateBonusAttributes) { }
    }
}

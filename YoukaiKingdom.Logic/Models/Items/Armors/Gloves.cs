using System;

namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    [Serializable]
    public class Gloves : Armor
    {
        private const int DefaultDefense = 10;
        private const int DefaultLevel = 1;
        
        private Gloves() { }

        public Gloves(int id, string name, int level, int defensePoints, bool generateBonusAttributes = true) : base(id, name, level, defensePoints, generateBonusAttributes) { }

        public Gloves(int id, string name, bool generateBonusAttributes = true) : base(id, name, DefaultLevel, DefaultDefense, generateBonusAttributes) { }
    }
}

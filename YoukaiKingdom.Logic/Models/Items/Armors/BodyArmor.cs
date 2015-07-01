using System;

namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    [Serializable]
    public class BodyArmor : Armor
    {
        private const int DefaultDefense = 50;
        private const int DefaultLevel = 1;

        public BodyArmor()
        { 
        }


        public BodyArmor(int id, string name, int level, int defensePoints, bool generateBonusAttributes = true)
            : base(id, name, level, defensePoints, generateBonusAttributes)
        {
        }

        public BodyArmor(int id, string name, bool generateBonusAttributes = true)
            : base(id, name, DefaultLevel, DefaultDefense, generateBonusAttributes)
        {
        }
    }
}

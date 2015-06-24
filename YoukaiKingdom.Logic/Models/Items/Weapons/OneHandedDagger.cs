﻿namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    using YoukaiKingdom.Logic.Interfaces;

    public class OneHandedDagger : Weapon, IOffhand
    {
        private const int DefaultAttackPoints = 40;

        private const int DefaultLevel = 1;

        public OneHandedDagger(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true) 
            : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }

        public OneHandedDagger(int id, string name, int attackSpeed, bool generateBonusAttributes = true) 
            : this(id, name, DefaultLevel, DefaultAttackPoints, attackSpeed, generateBonusAttributes) { }
    }
}
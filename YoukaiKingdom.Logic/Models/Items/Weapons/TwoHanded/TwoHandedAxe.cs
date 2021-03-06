﻿using System;

namespace YoukaiKingdom.Logic.Models.Items.Weapons.TwoHanded
{
    [Serializable]
    public class TwoHandedAxe : TwoHandedWeapon
    {
        private const int DefaultAttackPoints = 120;
        private const int DefaultLevel = 1;

        private TwoHandedAxe() { }

        public TwoHandedAxe(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true)
            : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }

        public TwoHandedAxe(int id, string name, int attackSpeed, bool generateBonusAttributes = true)
            : this(id, name, DefaultLevel, DefaultAttackPoints, attackSpeed, generateBonusAttributes) { }
    }
}

﻿namespace YoukaiKingdom.Logic.Interfaces
{
    using YoukaiKingdom.Logic.Models.Items.BonusAttributes;

    public interface IWeapon
    {
        int AttackPoints { get; set; }

        int AttackSpeed { get; set; }

        BonusAttributes Bonus { get; set; }
    }
}

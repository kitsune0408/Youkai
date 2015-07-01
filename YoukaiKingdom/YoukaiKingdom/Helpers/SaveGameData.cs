using System;
using System.Collections;
using System.Collections.Generic;
using YoukaiKingdom.Logic.Interfaces;
using YoukaiKingdom.Logic.Models.Characters.Heroes;
using YoukaiKingdom.Logic.Models.Inventory;
using YoukaiKingdom.Logic.Models.Items;
using YoukaiKingdom.Logic.Models.Items.Armors;
using YoukaiKingdom.Logic.Models.Items.Potions;
using YoukaiKingdom.Logic.Models.Items.Weapons;

namespace YoukaiKingdom.Helpers
{
    [Serializable]
    public struct SaveGameData
    {
        public string PlayerName;
        public CharacterType PlayerType;
        public int PlayerExperiencePoints;
        public int PlayerLevel;
        public int MaxHealth;
        public int CurrentHealth;
        public int CurrentMana;
        public int MaxMana;
        public int AttackPoints;
        public int DefencePoints;
        public Helmet Helmet;
        public Boots Boots;
        public Gloves Gloves;
        public Weapon MainHandWeapon;
        public Shield OffhandShield;
        public Weapon OffHandWeapon;
        public BodyArmor Armor;     
        public ArrayList BagItems;
        public LevelNumber LevelNumber;

    }
}

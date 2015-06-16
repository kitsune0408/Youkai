﻿namespace YoukaiKingdom.Logic.Models.Characters
{
    using YoukaiKingdom.Logic.Interfaces;

    public abstract class Character : ICharacter
    {
        protected Character(string name, int maxHealth, int maxMana, int damage, int armor)
        {
            this.Name = name;
            this.MaxHealth = maxHealth;
            this.MaxMana = maxMana;
            this.Health = maxHealth;
            this.Mana = maxMana;
            this.Damage = damage;
            this.Armor = armor;
        }

        public string Name { get; set; }

        public int MaxHealth { get; set; }

        public int MaxMana { get; set; }

        public int Damage { get; set; }

        public int Armor { get; set; }

        public int Health { get; set; }

        public int Mana { get; set; }

        public abstract void Hit(ICharacter target);

    }
}
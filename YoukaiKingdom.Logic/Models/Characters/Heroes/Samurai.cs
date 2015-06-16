namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Items.Weapons;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;
    using YoukaiKingdom.Logic.Interfaces;
   
    public class Samurai : Hero
    {
        private const int DefaultHealth = 300;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 100;
        private const int DefaultArmor = 100;

        public Samurai(string name) : base(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Samurai(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public override void Hit(ICharacter target)
        {
            throw new NotImplementedException();
        }
        public override void ApplyArmorEffects(Armor armor)
        {
            base.ApplyArmorEffects(armor);
        }
        public override void ApplyHealthEffects(HealingPotion health)
        {
            base.ApplyHealthEffects(health);
        }
        public override void ApplyManaEffects(ManaPotion mana)
        {
            base.ApplyManaEffects(mana);
        }
        public override void ApplyWeaponEffects(Weapon weapon)
        {
            base.ApplyWeaponEffects(weapon);
        }
        public override void RemoveHealthEffects(NPCs.Npc damage, Armor armor)
        {
            base.RemoveHealthEffects(damage, armor);
        }
    }
}

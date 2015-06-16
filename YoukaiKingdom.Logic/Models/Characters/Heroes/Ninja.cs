namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Items.Weapons;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;
    public class Ninja : Hero
    {
        private const int DefaultHealth = 100;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 90;
        private const int DefaultArmor = 60;

        public Ninja(string name) : base(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Ninja(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public override void Hit(ICharacter target)
        {
            throw new System.NotImplementedException();
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

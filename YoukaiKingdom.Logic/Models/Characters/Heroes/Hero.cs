namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Inventar;
    using YoukaiKingdom.Logic.Models.Items.Weapons;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;

    public abstract class Hero : Character
    {
        protected Hero(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor)
        {
            this.Inventar = new Inventar();
        }
        public virtual void ApplyWeaponEffects(Weapon weapon)
        {
            this.Damage = weapon.AttackPoints;
        }
        public virtual void ApplyArmorEffects(Armor armor)
        {
            this.Armor = armor.DefensePoints;
        }
        public virtual void ApplyManaEffects(ManaPotion mana)
        {
            this.Mana += mana.ManaPoints;
        }
        public virtual void ApplyHealthEffects(HealingPotion health)
        {
            this.Health += health.HealingPoints;
        }
        public virtual void RemoveHealthEffects(Npc damage, Armor armor)
        {
            this.Health -= damage.Damage;
            this.Health += armor.DefensePoints;
        }
        public override void Hit(ICharacter target)
        {
            if (target is Npc)
            {
                //TODO
            }
        }

        public void AdjustBonusAttributes(IBonusAttributes atributes)
        {
            //TODO
        }

        public Inventar Inventar { get; set; }
        //TODO
        private void AdjustEquipedItemStats()
        {
            //if (this.MainHandWeapon != null)
            //{
            //    this.
            //}
        }

    }
}

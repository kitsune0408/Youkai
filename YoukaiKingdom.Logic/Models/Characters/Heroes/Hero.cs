namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Inventory;
    using YoukaiKingdom.Logic.Models.Items.Weapons;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;

    public abstract class Hero : Character
    {
        protected Hero(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor)
        {
            this.Inventory = new Inventory();
        }

        public Inventory Inventory { get; set; }

        public void ApplyDamagePoints(Weapon weapon)
        {
            this.Damage += weapon.AttackPoints;
        }

        public void ApplyArmorPoints(Armor armor)
        {
            this.Armor += armor.DefensePoints;
        }

        public void ApplyManaPoints(ManaPotion mana)
        {
            this.Mana = Math.Min(this.MaxMana, this.Mana + mana.ManaPoints);
        }

        public void ApplyHealthPoints(HealingPotion health)
        {
            this.Health = Math.Min(this.MaxHealth, this.Health + health.HealingPoints);
        }

        public void RemoveHealthPoints(int damage, AttackType type)
        {
            if (type == AttackType.Physical)
            {
                this.Health -= damage - (this.Armor / 2);
            }
            else
            {
                this.Health -= damage;
            }
        }

        public override void Hit(ICharacter target)
        {
            if (target is Npc)
            {
                var targetNpc = (Npc)target;
                targetNpc.ReceiveHit(this);
            }
        }

        public override void ReceiveHit(ICharacter enemy)
        {
            if (enemy is Npc)
            {
                var enemyNpc = (Npc)enemy;

                if (enemyNpc is NpcMage)
                {
                    this.RemoveHealthPoints(enemyNpc.Damage, AttackType.Magical);
                }
                else if (enemyNpc is NpcRogue || enemyNpc is NpcWarrior)
                {
                    this.RemoveHealthPoints(enemyNpc.Damage, AttackType.Physical);
                }
            }
        }

        public void AdjustBonusAttributes(IBonusAttributes atributes)
        {
            //TODO
        }


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

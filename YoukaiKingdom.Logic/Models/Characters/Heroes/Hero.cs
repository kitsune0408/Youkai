namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Inventory;
    using YoukaiKingdom.Logic.Models.Items;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;

    public abstract class Hero : Character
    {
        private const int OffhandPenaltyDamage = 30;

        protected Hero(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor)
        {
            this.Inventory = new Inventory();
        }

        public Inventory Inventory { get; set; }

        #region Apply stats

        public void ApplyDamagePoints(int damage)
        {
            this.Damage += damage;
        }

        public void ApplyArmorPoints(int armor)
        {
            this.Armor += armor;
        }

        public void ApplyManaPoints(ManaPotion mana)
        {
            this.Mana = Math.Min(this.MaxMana, this.Mana + mana.ManaPoints);
        }

        public void ApplyHealthPoints(HealingPotion health)
        {
            this.Health = Math.Min(this.MaxHealth, this.Health + health.HealingPoints);
        }

        #endregion Apply stats

        #region Remove stats

        public void RemoveDamagePoints(int weaponDamage)
        {
            this.Damage -= weaponDamage;
        }

        public void RemoveArmorPoints(int armor)
        {
            this.Armor -= armor;
        }

        public void RemoveHealthPoints(int damage, AttackType type)
        {
            if (type == AttackType.Physical)
            {
                this.Health -= damage - (this.Armor - (1 * 50)); //1 e levela
            }
            else
            {
                this.Health -= damage;
            }
        }

        public bool RemoveManaPointsAfterCast(int level)
        {
            if (this.Mana < (level * 50))
            {
                return false;
            }

            this.Mana -= (level * 50);

            return true;
        }

        #endregion Remove stats

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

        public void AdjustBonusAttributes(IBonusAttributes attributes)
        {
            if (attributes.HasBonuses)
            {
                if (attributes.픡ditionalArmor > 0)
                {
                    this.Armor += attributes.픡ditionalArmor;
                }

                if (attributes.픡ditionalDamage > 0)
                {
                    this.Damage += attributes.픡ditionalDamage;
                }

                if (attributes.픡ditionalHealth > 0)
                {
                    this.MaxHealth += attributes.픡ditionalHealth;
                }

                if (attributes.픡ditionalMana > 0)
                {
                    this.MaxMana += attributes.픡ditionalMana;
                }
            }
        }

        public void RemoveBonusAttributes(IBonusAttributes attributes)
        {
            if (attributes.HasBonuses)
            {
                if (attributes.픡ditionalArmor > 0)
                {
                    this.Armor -= attributes.픡ditionalArmor;
                }

                if (attributes.픡ditionalDamage > 0)
                {
                    this.Damage -= attributes.픡ditionalDamage;
                }

                if (attributes.픡ditionalHealth > 0)
                {
                    this.MaxHealth -= attributes.픡ditionalHealth;
                }

                if (attributes.픡ditionalMana > 0)
                {
                    this.MaxMana -= attributes.픡ditionalMana;
                }
            }
        }

        public void AdjustEquipedItemStats()
        {
            if (this.Inventory.MainHandWeapon != null)
            {
                this.ApplyDamagePoints(this.Inventory.MainHandWeapon.AttackPoints);
            }

            if (this.Inventory.OffHand != null && this.Inventory.OffHand is IWeapon)
            {
                var offhand = (IWeapon)this.Inventory.OffHand;

                this.ApplyDamagePoints(offhand.AttackPoints - OffhandPenaltyDamage);
            }

            if (this.Inventory.BodyArmor != null)
            {
                this.ApplyArmorPoints(this.Inventory.BodyArmor.DefensePoints);
            }

            if (this.Inventory.Boots != null)
            {
                this.ApplyArmorPoints(this.Inventory.Boots.DefensePoints);
            }

            if (this.Inventory.Gloves != null)
            {
                this.ApplyArmorPoints(this.Inventory.Gloves.DefensePoints);
            }

            if (this.Inventory.Helmet != null)
            {
                this.ApplyArmorPoints(this.Inventory.Helmet.DefensePoints);
            }

            if (this.Inventory.OffHand != null && this.Inventory.OffHand is Shield)
            {
                var offhand = (IArmor)this.Inventory.OffHand;
                this.ApplyArmorPoints(offhand.DefensePoints);
            }
        }

        #region Equip Items

        public void ReplaceMainHand(Item replacement)
        {
            if (replacement is IWeapon)
            {
                if (this.Inventory.MainHandWeapon != null)
                {
                    var replace = (IWeapon)replacement;
                    this.RemoveDamagePoints(this.Inventory.MainHandWeapon.AttackPoints);
                    this.Inventory.AddItemToBag((Item)this.Inventory.MainHandWeapon);
                    this.Inventory.EquipMainHand((IWeapon)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyDamagePoints(replace.AttackPoints);
                }
                else
                {
                    var replace = (IWeapon)replacement;
                    this.Inventory.EquipMainHand((IWeapon)replacement);
                    this.ApplyDamagePoints(replace.AttackPoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                }
            }
        }

        public void ReplaceOffHand(Item replacement)
        {
            if (replacement is IOffhand)
            {
                if (replacement is IWeapon)
                {
                    if (this.Inventory.OffHand != null)
                    {
                        var replace = (IWeapon)replacement;
                        this.RemoveDamagePoints(this.Inventory.MainHandWeapon.AttackPoints - OffhandPenaltyDamage);
                        this.Inventory.AddItemToBag((Item)this.Inventory.OffHand);
                        this.Inventory.EquipOffHand((IOffhand)replacement);
                        this.Inventory.RemoveItemFromBag(replacement);
                        this.ApplyDamagePoints(replace.AttackPoints - OffhandPenaltyDamage);
                    }
                    else
                    {
                        var replace = (IWeapon)replacement;
                        this.Inventory.EquipOffHand((IOffhand)replacement);
                        this.ApplyDamagePoints(replace.AttackPoints - OffhandPenaltyDamage);
                        this.Inventory.RemoveItemFromBag(replacement);
                    }
                }
                else if (replacement is IArmor)
                {
                    if (this.Inventory.OffHand != null)
                    {
                        var replace = (IArmor)replacement;
                        var offHand = (IArmor)this.Inventory.OffHand;
                        this.RemoveArmorPoints(offHand.DefensePoints);
                        this.Inventory.AddItemToBag((Item)this.Inventory.OffHand);
                        this.Inventory.EquipOffHand((IOffhand)replacement);
                        this.Inventory.RemoveItemFromBag(replacement);
                        this.ApplyArmorPoints(replace.DefensePoints);
                    }
                    else
                    {
                        var replace = (IArmor)replacement;
                        this.Inventory.EquipOffHand((IOffhand)replace);
                        this.ApplyArmorPoints(replace.DefensePoints);
                        this.Inventory.RemoveItemFromBag(replacement);
                    }
                }
            }
        }

        public void ReplaceHelmet(Item replacement)
        {
            if (replacement is Helmet)
            {
                if (this.Inventory.Helmet != null)
                {
                    var replace = (IArmor)replacement;
                    this.RemoveArmorPoints(this.Inventory.Helmet.DefensePoints);
                    this.Inventory.AddItemToBag(this.Inventory.Helmet);
                    this.Inventory.EquipArmor((Helmet)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                }
                else
                {
                    var replace = (IArmor)replacement;
                    this.Inventory.EquipArmor((Helmet)replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                }
            }
        }

        public void ReplaceBodyArmor(Item replacement)
        {
            if (replacement is BodyArmor)
            {
                if (this.Inventory.BodyArmor != null)
                {
                    var replace = (IArmor)replacement;
                    this.RemoveArmorPoints(this.Inventory.BodyArmor.DefensePoints);
                    this.Inventory.AddItemToBag(this.Inventory.BodyArmor);
                    this.Inventory.EquipArmor((BodyArmor)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                }
                else
                {
                    var replace = (IArmor)replacement;
                    this.Inventory.EquipArmor((BodyArmor)replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                }
            }
        }

        public void ReplaceBoots(Item replacement)
        {
            if (replacement is Boots)
            {
                if (this.Inventory.Boots != null)
                {
                    var replace = (IArmor)replacement;
                    this.RemoveArmorPoints(this.Inventory.Boots.DefensePoints);
                    this.Inventory.AddItemToBag(this.Inventory.Boots);
                    this.Inventory.EquipArmor((Boots)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                }
                else
                {
                    var replace = (IArmor)replacement;
                    this.Inventory.EquipArmor((Boots)replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                }
            }
        }

        public void ReplaceGloves(Item replacement)
        {
            if (replacement is Gloves)
            {
                if (this.Inventory.Boots != null)
                {
                    var replace = (IArmor)replacement;
                    this.RemoveArmorPoints(this.Inventory.Gloves.DefensePoints);
                    this.Inventory.AddItemToBag(this.Inventory.Gloves);
                    this.Inventory.EquipArmor((Gloves)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                }
                else
                {
                    var replace = (IArmor)replacement;
                    this.Inventory.EquipArmor((Gloves)replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                }
            }
        }

        #endregion Equip Items
    }
}

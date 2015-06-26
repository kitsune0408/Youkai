namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System;
   
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Inventory;
    using YoukaiKingdom.Logic.Models.Items;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;

    public abstract class Hero : Character
    {
        private const int OffhandPenaltyDamage = 30;

        private const int DefaultLevel = 1;

        private const int DefaultHitRange = 1;

        private static readonly Location DefaultLocation = new Location(250, 250);

        protected Hero(string name, int health, int mana, int damage, int armor, int attackSpeed)
            : base(DefaultLevel, name, health, mana, damage, armor, attackSpeed, DefaultHitRange, DefaultLocation)
        {
            this.Inventory = new Inventory();
        }

        public Inventory Inventory { get; set; }

        public int DamageGotten { get; private set; }

        #region Apply stats

        private void ApplyDamagePoints(int damage)
        {
            this.Damage += damage;
        }

        private void ApplyArmorPoints(int armor)
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

        private void ApplyAttackSpeed(int attackSpeed)
        {
            this.AttackSpeed = attackSpeed;
        }

        #endregion Apply stats

        #region Remove stats

        private void RemoveDamagePoints(int weaponDamage)
        {
            this.Damage -= weaponDamage;
        }

        private void RemoveArmorPoints(int armor)
        {
            this.Armor -= armor;
        }

        private void RemoveHealthPoints(int damage)
        {
            this.Health -= damage;
            if (this.Health < 0)
            {
                this.Health = 0;
            }
        }

        protected bool RemoveManaPointsAfterCast(int manaCost)
        {
            if (this.Mana < manaCost)
            {
                return false;
            }

            this.Mana -= manaCost;

            return true;
        }

        #endregion Remove stats

        public override void ReceiveHit(int damage, AttackType type)
        {
            if (type == AttackType.Physical)
            {
                int dmg = Math.Max(0, damage - (this.Armor - (this.Level * 50)));
                this.RemoveHealthPoints(dmg);
                this.DamageGotten = dmg;
            }
            else if (type == AttackType.Magical)
            {
                this.RemoveHealthPoints(damage);
                this.DamageGotten = damage;
            }
        }

        public void AdjustBonusAttributes(IBonusAttributes attributes)
        {
            if (attributes != null && attributes.HasBonuses)
            {
                if (attributes.АdditionalArmor > 0)
                {
                    this.Armor += attributes.АdditionalArmor;
                }

                if (attributes.АdditionalDamage > 0)
                {
                    this.Damage += attributes.АdditionalDamage;
                }

                if (attributes.АdditionalHealth > 0)
                {
                    this.MaxHealth += attributes.АdditionalHealth;
                }

                if (attributes.АdditionalMana > 0)
                {
                    this.MaxMana += attributes.АdditionalMana;
                }
            }
        }

        public void RemoveBonusAttributes(IBonusAttributes attributes)
        {
            if (attributes != null && attributes.HasBonuses)
            {
                if (attributes.АdditionalArmor > 0)
                {
                    this.Armor -= attributes.АdditionalArmor;
                }

                if (attributes.АdditionalDamage > 0)
                {
                    this.Damage -= attributes.АdditionalDamage;
                }

                if (attributes.АdditionalHealth > 0)
                {
                    this.MaxHealth -= attributes.АdditionalHealth;
                }

                if (attributes.АdditionalMana > 0)
                {
                    this.MaxMana -= attributes.АdditionalMana;
                }
            }
        }

        public void AdjustEquipedItemStats()
        {
            if (this.Inventory.MainHandWeapon != null)
            {
                this.ApplyDamagePoints(this.Inventory.MainHandWeapon.AttackPoints);
                this.ApplyAttackSpeed(this.Inventory.MainHandWeapon.AttackSpeed);
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

        public void ReplaceMainHand(Item replacement, CharacterType type)
        {
            if (replacement is IWeapon)
            {
                if (this.Inventory.MainHandWeapon != null)
                {
                    var replace = (IWeapon)replacement;
                    this.RemoveDamagePoints(this.Inventory.MainHandWeapon.AttackPoints);
                    this.SetDefaultAttackSpeed(type);
                    this.RemoveBonusAttributes(this.Inventory.MainHandWeapon.Bonus);
                    this.Inventory.AddItemToBag((Item)this.Inventory.MainHandWeapon);
                    this.Inventory.EquipMainHand((IWeapon)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyDamagePoints(replace.AttackPoints);
                    this.ApplyAttackSpeed(replace.AttackSpeed);
                    this.AdjustBonusAttributes(replace.Bonus);
                }
                else
                {
                    var replace = (IWeapon)replacement;
                    this.Inventory.EquipMainHand((IWeapon)replacement);
                    this.ApplyDamagePoints(replace.AttackPoints);
                    this.ApplyAttackSpeed(replace.AttackSpeed);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.AdjustBonusAttributes(replace.Bonus);
                }
            }
        }

        private void SetDefaultAttackSpeed(CharacterType type)
        {
            if (type == CharacterType.Samurai)
            {
                this.AttackSpeed = Samurai.DefaultSamuraiAttackSpeed;
            }
            else if (type == CharacterType.Monk)
            {
                this.AttackSpeed = Monk.DefaultMonkAttackSpeed;
            }
            else if (type == CharacterType.Ninja)
            {
                this.AttackSpeed = Ninja.DefaultNinjaAttackSpeed;
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
                        var offHand = (IWeapon)this.Inventory.OffHand;
                        this.RemoveDamagePoints(offHand.AttackPoints - OffhandPenaltyDamage);
                        this.RemoveBonusAttributes(offHand.Bonus);
                        this.Inventory.AddItemToBag((Item)this.Inventory.OffHand);
                        this.Inventory.EquipOffHand((IOffhand)replacement);
                        this.Inventory.RemoveItemFromBag(replacement);
                        this.ApplyDamagePoints(replace.AttackPoints - OffhandPenaltyDamage);
                        this.AdjustBonusAttributes(replace.Bonus);
                    }
                    else
                    {
                        var replace = (IWeapon)replacement;
                        this.Inventory.EquipOffHand((IOffhand)replacement);
                        this.ApplyDamagePoints(replace.AttackPoints - OffhandPenaltyDamage);
                        this.Inventory.RemoveItemFromBag(replacement);
                        this.AdjustBonusAttributes(replace.Bonus);
                    }
                }
                else if (replacement is IArmor)
                {
                    if (this.Inventory.OffHand != null)
                    {
                        var replace = (IArmor)replacement;
                        var offHand = (IArmor)this.Inventory.OffHand;
                        this.RemoveArmorPoints(offHand.DefensePoints);
                        this.RemoveBonusAttributes(offHand.Bonus);
                        this.Inventory.AddItemToBag((Item)this.Inventory.OffHand);
                        this.Inventory.EquipOffHand((IOffhand)replacement);
                        this.Inventory.RemoveItemFromBag(replacement);
                        this.ApplyArmorPoints(replace.DefensePoints);
                        this.AdjustBonusAttributes(replace.Bonus);
                    }
                    else
                    {
                        var replace = (IArmor)replacement;
                        this.Inventory.EquipOffHand((IOffhand)replace);
                        this.ApplyArmorPoints(replace.DefensePoints);
                        this.Inventory.RemoveItemFromBag(replacement);
                        this.AdjustBonusAttributes(replace.Bonus);
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
                    this.RemoveBonusAttributes(this.Inventory.Helmet.Bonus);
                    this.Inventory.AddItemToBag(this.Inventory.Helmet);
                    this.Inventory.EquipArmor((Helmet)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.AdjustBonusAttributes(replace.Bonus);
                }
                else
                {
                    var replace = (IArmor)replacement;
                    this.Inventory.EquipArmor((Helmet)replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.AdjustBonusAttributes(replace.Bonus);
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
                    this.RemoveBonusAttributes(this.Inventory.BodyArmor.Bonus);
                    this.Inventory.AddItemToBag(this.Inventory.BodyArmor);
                    this.Inventory.EquipArmor((BodyArmor)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.AdjustBonusAttributes(replace.Bonus);
                }
                else
                {
                    var replace = (IArmor)replacement;
                    this.Inventory.EquipArmor((BodyArmor)replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.AdjustBonusAttributes(replace.Bonus);
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
                    this.RemoveBonusAttributes(this.Inventory.Boots.Bonus);
                    this.Inventory.AddItemToBag(this.Inventory.Boots);
                    this.Inventory.EquipArmor((Boots)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.AdjustBonusAttributes(replace.Bonus);
                }
                else
                {
                    var replace = (IArmor)replacement;
                    this.Inventory.EquipArmor((Boots)replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.AdjustBonusAttributes(replace.Bonus);
                }
            }
        }

        public void ReplaceGloves(Item replacement)
        {
            if (replacement is Gloves)
            {
                if (this.Inventory.Gloves != null)
                {
                    var replace = (IArmor)replacement;
                    this.RemoveArmorPoints(this.Inventory.Gloves.DefensePoints);
                    this.RemoveBonusAttributes(this.Inventory.Gloves.Bonus);
                    this.Inventory.AddItemToBag(this.Inventory.Gloves);
                    this.Inventory.EquipArmor((Gloves)replacement);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.AdjustBonusAttributes(replace.Bonus);
                }
                else
                {
                    var replace = (IArmor)replacement;
                    this.Inventory.EquipArmor((Gloves)replacement);
                    this.ApplyArmorPoints(replace.DefensePoints);
                    this.Inventory.RemoveItemFromBag(replacement);
                    this.AdjustBonusAttributes(replace.Bonus);
                }
            }
        }

        #endregion Equip Items

        #region UnEquip Items

        public void RemoveMainHand(CharacterType type)
        {
            if (this.Inventory.MainHandWeapon != null && !this.Inventory.IsFull)
            {
                this.RemoveDamagePoints(this.Inventory.MainHandWeapon.AttackPoints);
                this.RemoveBonusAttributes(this.Inventory.MainHandWeapon.Bonus);
                this.SetDefaultAttackSpeed(type);
                this.Inventory.AddItemToBag((Item)this.Inventory.MainHandWeapon);
                this.Inventory.UnEquipMainHand();
            }
        }

        public void RemoveOffHand()
        {
            if (this.Inventory.OffHand != null && !this.Inventory.IsFull)
            {
                if (this.Inventory.OffHand is IWeapon)
                {
                    var offHand = (IWeapon)this.Inventory.OffHand;
                    this.RemoveDamagePoints(offHand.AttackPoints - OffhandPenaltyDamage);
                    this.RemoveBonusAttributes(offHand.Bonus);
                    this.Inventory.AddItemToBag((Item)this.Inventory.OffHand);
                    this.Inventory.UnEquipOffHand();
                }
                else if (this.Inventory.OffHand is IArmor)
                {
                    var offHand = (IArmor)this.Inventory.OffHand;
                    this.RemoveArmorPoints(offHand.DefensePoints);
                    this.RemoveBonusAttributes(offHand.Bonus);
                    this.Inventory.AddItemToBag((Item)this.Inventory.OffHand);
                    this.Inventory.UnEquipOffHand();
                }
            }
        }

        public void RemoveHelmet()
        {
            if (this.Inventory.Helmet != null && !this.Inventory.IsFull)
            {
                this.RemoveArmorPoints(this.Inventory.Helmet.DefensePoints);
                this.RemoveBonusAttributes(this.Inventory.Helmet.Bonus);
                this.Inventory.AddItemToBag(this.Inventory.Helmet);
                this.Inventory.UnEquipHelmet();
            }
        }

        public void RemoveBoots()
        {
            if (this.Inventory.Boots != null && !this.Inventory.IsFull)
            {
                this.RemoveArmorPoints(this.Inventory.Boots.DefensePoints);
                this.RemoveBonusAttributes(this.Inventory.Boots.Bonus);
                this.Inventory.AddItemToBag(this.Inventory.Boots);
                this.Inventory.UnEquipBoots();
            }
        }

        public void RemoveGloves()
        {
            if (this.Inventory.Gloves != null && !this.Inventory.IsFull)
            {
                this.RemoveArmorPoints(this.Inventory.Gloves.DefensePoints);
                this.RemoveBonusAttributes(this.Inventory.Gloves.Bonus);
                this.Inventory.AddItemToBag(this.Inventory.Gloves);
                this.Inventory.UnEquipGloves();
            }
        }

        public void RemoveBodyArmor()
        {
            if (this.Inventory.BodyArmor != null && !this.Inventory.IsFull)
            {
                this.RemoveArmorPoints(this.Inventory.BodyArmor.DefensePoints);
                this.RemoveBonusAttributes(this.Inventory.BodyArmor.Bonus);
                this.Inventory.AddItemToBag(this.Inventory.BodyArmor);
                this.Inventory.UnEquipBodyArmor();
            }
        }

        #endregion UnEquip Items
    }
}

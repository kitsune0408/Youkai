namespace YoukaiKingdom.Logic.Models.Inventory
{
    using System.Collections.Generic;
    using System.Linq;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Items;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded;

    public class Inventory
    {
        private const int MaxBagSlots = 36;

        private List<Item> bag;

        public Inventory()
        {
            this.bag = new List<Item>(MaxBagSlots);
        }

        public IWeapon MainHandWeapon { get; private set; }
        public IOffhand OffHand { get; private set; }
        public BodyArmor BodyArmor { get; private set; }
        public Boots Boots { get; private set; }
        public Helmet Helmet { get; private set; }
        public Gloves Gloves { get; private set; }
        public bool IsFull { get; private set; }

        public List<Item> Bag
        {
            get
            {
                return this.bag;
            }
        }

        public void AddItemToBag(Item item)
        {
            if (!this.IsFull)
            {
                 this.bag.Add(item);
                 if (this.bag.Count == MaxBagSlots)
                 {
                     this.IsFull = true;
                 }
            }
        }

        public void RemoveItemFromBag(Item item)
        {
            var result = this.Bag.FirstOrDefault(x => x.Id == item.Id);
            if (result != null)
            {
                this.bag.Remove(result);
                this.IsFull = false;
            }
        }

        public void EquipMainHand(IWeapon weapon)
        {
            this.MainHandWeapon = weapon;
        }

        public void UnEquipMainHand()
        {
            this.MainHandWeapon = null;
        }

        //TODO
        public void EquipOffHand(IOffhand offhand)
        {
            if (this.MainHandWeapon is OneHandedWeapon || this.MainHandWeapon == null)
            {
                this.OffHand = offhand;
            }
        }

        public void UnEquipOffHand()
        {
            this.OffHand = null;
        }

        //TODO
        public void EquipArmor(IArmor armor)
        {
            if (armor == null)
            {
                return;
            }

            if (armor is Helmet)
            {
                this.Helmet = (Helmet)armor;
            }
            else if (armor is BodyArmor)
            {
                this.BodyArmor = (BodyArmor)armor;
            }
            else if (armor is Boots)
            {
                this.Boots = (Boots)armor;
            }
            else if (armor is Gloves)
            {
                this.Gloves = (Gloves)armor;
            }
        }

        public void UnEquipBodyArmor()
        {
            this.BodyArmor = null;
        }

        public void UnEquipHelmet()
        {
            this.Helmet = null;
        }

        public void UnEquipBoots()
        {
            this.Boots = null;
        }

        public void UnEquipGloves()
        {
            this.Gloves = null;
        }
    }
}

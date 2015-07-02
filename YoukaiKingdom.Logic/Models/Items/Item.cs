using System;
using System.Runtime.Remoting.Messaging;
using System.Xml.Serialization;
using YoukaiKingdom.Logic.Models.Items.Armors;
using YoukaiKingdom.Logic.Models.Items.Potions;
using YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded;
using YoukaiKingdom.Logic.Models.Items.Weapons.TwoHanded;

namespace YoukaiKingdom.Logic.Models.Items
{
    using YoukaiKingdom.Logic.Interfaces;
    
    [Serializable]
    [XmlInclude(typeof(TwoHandedSword))]
    [XmlInclude(typeof(TwoHandedStaff))]
    [XmlInclude(typeof(TwoHandedAxe))]
    [XmlInclude(typeof(OneHandedDagger))]
    [XmlInclude(typeof(OneHandedSword))]
    [XmlInclude(typeof(BodyArmor))]
    [XmlInclude(typeof(Helmet))]
    [XmlInclude(typeof(Boots))]
    [XmlInclude(typeof(Shield))]
    [XmlInclude(typeof(HealingPotion))]
    [XmlInclude(typeof(ManaPotion))]
    public abstract class Item : IItem
    {
        protected Item(int id, string name, int level)
        {
            this.Id = id;
            this.Name = name;
            this.Level = level;
        }

        protected Item()
        {
        }

        public int Id { get; set; }

        public int Level { get; set; }

        public string Name { get; set; }

    }
}

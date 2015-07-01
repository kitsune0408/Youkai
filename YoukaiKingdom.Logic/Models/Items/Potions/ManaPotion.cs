using System;

namespace YoukaiKingdom.Logic.Models.Items.Potions
{
    [Serializable]
    public class ManaPotion : Potion
    {
        public ManaPotion() { }

        public ManaPotion(int id, string name, int level, int manaPoints)
            : base(id, name, level)
        {
            this.ManaPoints = manaPoints;
        }

        public int ManaPoints { get; set; }
    }
}

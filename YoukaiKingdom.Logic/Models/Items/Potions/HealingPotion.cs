using System;

namespace YoukaiKingdom.Logic.Models.Items.Potions
{
    [Serializable]
    public class HealingPotion : Potion
    {
        public HealingPotion(int id, string name, int level, int healingPoints)
            : base(id, name, level)
        {
            this.HealingPoints = healingPoints;
        }

        private HealingPotion()
        {
        }

        public int HealingPoints { get; set; }
    }
}

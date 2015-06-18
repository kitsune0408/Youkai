namespace YoukaiKingdom.Logic.Models.Items.Potions
{
    public class HealingPotion : Potion
    {
        public HealingPotion(int id, string name, int level, int healingPoints)
            : base(id, name, level)
        {
            this.HealingPoints = healingPoints;
        }

        public int HealingPoints { get; set; }
    }
}

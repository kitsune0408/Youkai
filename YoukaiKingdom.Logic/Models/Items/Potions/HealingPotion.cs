namespace YoukaiKingdom.Logic.Models.Items.Potions
{
    public class HealingPotion : Potion
    {
        public HealingPotion(string name, int level,int healingPoints)
            : base(name, level)
        {
            this.HealingPoints = healingPoints;
        }

        public int HealingPoints { get; set; }
    }
}

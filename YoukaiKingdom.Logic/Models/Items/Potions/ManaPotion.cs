namespace YoukaiKingdom.Logic.Models.Items.Potions
{
    public class ManaPotion : Potion
    {
        public ManaPotion(string name, int level, int manaPoints)
            : base(name, level)
        {
            this.ManaPoints = manaPoints;
        }

        public int ManaPoints { get; set; }
    }
}

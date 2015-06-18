namespace YoukaiKingdom.Logic.Models.Items.Potions
{
    public class ManaPotion : Potion
    {
        public ManaPotion(int id, string name, int level, int manaPoints)
            : base(id, name, level)
        {
            this.ManaPoints = manaPoints;
        }

        public int ManaPoints { get; set; }
    }
}

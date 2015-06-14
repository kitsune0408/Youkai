namespace YoukaiKingdom.Logic.Models.Items.Potions
{
    public abstract class Potion : Item
    {
        protected Potion(string name, int level)
            : base(name, level)
        {
        }
    }
}

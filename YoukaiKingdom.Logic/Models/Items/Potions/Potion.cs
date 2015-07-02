using System;

namespace YoukaiKingdom.Logic.Models.Items.Potions
{
    [Serializable]
    public abstract class Potion : Item
    {
        protected Potion(int id, string name, int level)
            : base(id, name, level)
        {
        }

        protected Potion()
        {
        }
    }
}

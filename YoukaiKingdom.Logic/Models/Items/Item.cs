namespace YoukaiKingdom.Logic.Models.Items
{
    using YoukaiKingdom.Logic.Interfaces;

    public abstract class Item : IItem
    {
        protected Item(string name, int level)
        {
            this.Name = name;
            this.Level = level;
        }

        public int Level { get; set; }

        public string Name { get; set; }

    }
}

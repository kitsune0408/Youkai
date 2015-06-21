namespace YoukaiKingdom.Logic.Models.Items
{
    using YoukaiKingdom.Logic.Interfaces;

    public abstract class Item : IItem
    {
        protected Item(int id, string name, int level)
        {
            this.Id = id;
            this.Name = name;
            this.Level = level;
        }

        public int Id { get; set; }

        public int Level { get; set; }

        public string Name { get; set; }

    }
}

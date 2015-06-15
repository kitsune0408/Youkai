namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Inventar;

    public abstract class Hero : Character
    {
        protected Hero(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor)
        {
            this.Inventar = new Inventar();
        }

        public override void Hit(ICharacter target)
        {
            if (target is Npc)
            {
                //TODO
            }
        }

        public void AdjustBonusAttributes(IBonusAttributes atributes)
        {
            //TODO
        }

        public Inventar Inventar { get; set; }
        //TODO
        private void AdjustEquipedItemStats()
        {
            //if (this.MainHandWeapon != null)
            //{
            //    this.
            //}
        }

    }
}

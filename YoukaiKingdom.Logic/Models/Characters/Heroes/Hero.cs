namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;

    public abstract class Hero : Character
    {
        protected Hero(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

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
    }
}

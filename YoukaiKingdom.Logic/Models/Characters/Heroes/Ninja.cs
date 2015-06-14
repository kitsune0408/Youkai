namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using YoukaiKingdom.Logic.Interfaces;

    public class Ninja : Hero
    {
        public Ninja(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public override void Hit(ICharacter target)
        {
            throw new System.NotImplementedException();
        }
    }
}

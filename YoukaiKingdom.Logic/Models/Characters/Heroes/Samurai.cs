namespace YoukaiKingdom.Logic.Models.Characters
{
    using System;

    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    public class Samurai : Hero
    {
        public Samurai(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public override void Hit(Interfaces.ICharacter target)
        {
            throw new NotImplementedException();
        }
    }
}

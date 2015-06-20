namespace YoukaiKingdom.Logic.Models.Characters.Spells
{
    using YoukaiKingdom.Logic.Interfaces;

    public abstract class Spell : ISpell
    {
        protected Spell(int damage, int manaCost, double castInterval)
        {
            this.Damage = damage;
            this.ManaCost = manaCost;
            this.CastInterval = castInterval;
        }

        public int Damage { get; private set; }

        public int ManaCost { get; private set; }

        public double CastInterval { get; private set; }
    }
}

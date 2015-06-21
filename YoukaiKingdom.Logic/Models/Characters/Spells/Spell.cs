namespace YoukaiKingdom.Logic.Models.Characters.Spells
{
    using YoukaiKingdom.Logic.Interfaces;

    public abstract class Spell : ISpell
    {
        protected Spell(int damage, int manaCost, double castInterval, int spellRange)
        {
            this.Damage = damage;
            this.ManaCost = manaCost;
            this.CastInterval = castInterval;
            this.SpellRange = spellRange;
        }

        public int Damage { get; private set; }

        public int ManaCost { get; private set; }

        public double CastInterval { get; private set; }

        public int SpellRange { get; set; }
    }
}

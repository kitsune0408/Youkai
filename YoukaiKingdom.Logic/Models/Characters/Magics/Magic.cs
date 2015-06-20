namespace YoukaiKingdom.Logic.Models.Characters.Magics
{
    public abstract class Magic : IMagic
    {
        protected Magic(int damage, int manaCost, double castInterval)
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

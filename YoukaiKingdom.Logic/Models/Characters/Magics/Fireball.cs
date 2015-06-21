namespace YoukaiKingdom.Logic.Models.Characters.Magics
{
    public class Fireball : Magic
    {
        private const int DefaultDamage = 150;

        private const int DefaultManaCost = 50;

        private const double DefaultCastInterval = 2000; //milisec

        public static Fireball CreateFireball()
        {
            return new Fireball();
        }

        private Fireball(int damage = DefaultDamage, int manaCost = DefaultManaCost, double castInterval = DefaultCastInterval) : base(damage, manaCost, castInterval) { }

        public int Cast(int playerLevel)
        {
            return this.Damage + (playerLevel * 50);
        }
    }
}

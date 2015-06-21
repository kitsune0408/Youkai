namespace YoukaiKingdom.Logic.Models.Characters.Spells
{
    using System.Timers;

    public class Fireball : Spell
    {
        private const int DefaultDamage = 150;

        private const int DefaultManaCost = 50;

        private const double DefaultCastInterval = 3000; //milisec

        private const int DefaultSpellRange = 6;

        private Timer hitTimer;

        public static Fireball CreateFireball()
        {
            return new Fireball();
        }

        private Fireball(
            int damage = DefaultDamage,
            int manaCost = DefaultManaCost,
            double castInterval = DefaultCastInterval)
            : base(damage, manaCost, castInterval, DefaultSpellRange)
        {
            this.hitTimer = new Timer(DefaultCastInterval);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
            this.IsReady = true;
        }

        public bool IsReady { get; set; }

        public int Cast(int playerLevel)
        {
            this.hitTimer.Start();
            return this.Damage + (playerLevel * 50);
        }

        void HitTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.IsReady)
            {
                this.hitTimer.Stop();
            }

            this.IsReady = true;
        }
    }
}

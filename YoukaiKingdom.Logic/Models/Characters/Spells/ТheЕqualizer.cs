using System.Timers;
namespace YoukaiKingdom.Logic.Models.Characters.Spells
{
    class ТheЕqualizer : Spell
    {
        private const int DefaultDamage = 200;

        private const int DefaultManaCost = 5;

        private const double DefaultCastInterval = 2000; //milisec

        private const int DefaultSpellRange = 1;

        private Timer hitTimer;

        public static ТheЕqualizer CreateMagicHit(double castInterval = DefaultCastInterval)
        {
            return new ТheЕqualizer(DefaultDamage, DefaultManaCost, castInterval);
        }

        private ТheЕqualizer(
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

        public int Cast(int maxHealth, int health)
        {
            this.hitTimer.Start();
            return this.Damage + (maxHealth - health);

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

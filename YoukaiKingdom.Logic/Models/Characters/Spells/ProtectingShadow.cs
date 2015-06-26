namespace YoukaiKingdom.Logic.Models.Characters.Spells
{
    using System.Timers;

    public class ProtectingShadow : Spell
    {
        private const int DefaultDamage = 200;

        private const int DefaultManaCost = 10;

        private const double DefaultCastInterval = 5000; //milisec

        private const double DefaultDuration = 5000; //milisec

        private const int DefaultSpellRange = 0;

        private Timer intervalTimer;

        private Timer durationTimer;

        public static ProtectingShadow CreateProtectedOfDamage(double castInterval = DefaultCastInterval)
        {
            return new ProtectingShadow(DefaultDamage, DefaultManaCost, castInterval);
        }

        private ProtectingShadow(
            int damage = 0,
            int manaCost = DefaultManaCost,
            double castInterval = DefaultCastInterval)
            : base(damage, manaCost, castInterval, DefaultSpellRange)
        {
            this.durationTimer = new Timer(DefaultDuration);
            this.durationTimer.Elapsed += this.DurationTimerElapsed;
            this.intervalTimer = new Timer(DefaultCastInterval);
            this.intervalTimer.Elapsed += this.IntervalTimerElapsed;
            this.IsReady = true;

        }

        private void DurationTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.IsProtecting = false;
            this.intervalTimer.Start();
        }

        public bool IsReady { get; set; }

        public bool IsProtecting { get; private set; }

        public void Cast()
        {
            this.IsReady = false;
            this.IsProtecting = true;
            this.durationTimer.Start();
        }

        void IntervalTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.IsReady)
            {
                this.intervalTimer.Stop();
            }
            this.IsProtecting = false;
            this.durationTimer.Stop();
            this.IsReady = true;
        }
    }
}

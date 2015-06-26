using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using YoukaiKingdom.Logic.Models.Characters.Heroes;
namespace YoukaiKingdom.Logic.Models.Characters.Spells
{
    class ProtectingShadow : Spell
    {
        private const int DefaultDamage = 200;

        private const int DefaultManaCost = 10;

        private const double DefaultCastInterval = 8000; //milisec

        private const int DefaultSpellRange = 5;

        private Timer hitTimer;

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
            this.hitTimer = new Timer(DefaultCastInterval);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
            this.IsReady = true;

        }

        public bool IsReady { get; set; }

        public void Cast()
        {
            this.hitTimer.Start();

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

namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System.Timers;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Characters.Spells;
    public class Samurai : Hero
    {
        private const int DefaultHealth = 300;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 80;
        private const int DefaultArmor = 150;
        private const int DefaultAttackSpeed = 2000;
        private readonly ТheЕqualizer theЕqualizer;
        private Timer hitTimer;

        public Samurai(string name) : this(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Samurai(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor, DefaultAttackSpeed)
        {
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
            this.theЕqualizer = ТheЕqualizer.CreateMagicHit();
        }

        #region Default Values

        public static int DefaultSamuraiHealth
        {
            get
            {
                return DefaultHealth;
            }
        }

        public static int DefaultSamuraiArmor
        {
            get
            {
                return DefaultArmor;
            }
        }

        public static int DefaultSamuraiDamage
        {
            get
            {
                return DefaultDamage;
            }
        }

        public static int DefaultSamuraiMana
        {
            get
            {
                return DefaultMana;
            }
        }

        public static int DefaultSamuraiAttackSpeed
        {
            get
            {
                return DefaultAttackSpeed;
            }
        }

        public int MagicHitCastRange
        {
            get
            {
                return this.theЕqualizer.SpellRange;
            }
        }
        #endregion Default Values

        public override void Hit(ICharacter target)
        {
            if (target is Npc && this.IsReadyToAttack)
            {
                var targetNpc = (Npc)target;
                targetNpc.ReceiveHit(this.Damage, AttackType.Physical);
                this.IsReadyToAttack = false;
                this.hitTimer.Interval = this.AttackSpeed;
                this.hitTimer.Start();
            }
        }

        public bool EqualizerIsReady
        {
            get
            {
                return this.theЕqualizer.IsReady;
            }
        }
        public void CastЕqualizer(ICharacter enemy)
        {
            if (enemy is Npc && this.theЕqualizer.IsReady)
            {
                if (this.RemoveManaPointsAfterCast(this.theЕqualizer.ManaCost))
                {
                    enemy.ReceiveHit(this.theЕqualizer.Cast(this.MaxHealth, this.Health), AttackType.Physical);
                    this.theЕqualizer.IsReady = false;
                }
            }
        }

        private void HitTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.IsReadyToAttack)
            {
                this.hitTimer.Stop();
            }
            this.IsReadyToAttack = true;
        }
    }
}

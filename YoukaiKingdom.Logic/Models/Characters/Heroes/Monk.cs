using System;

namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System.Timers;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Characters.Spells;

    public class Monk : Hero
    {
        private const int DefaultHealth = 250;
        private const int DefaultMana = 400;
        private const int DefaultDamage = 50;
        private const int DefaultArmor = 20;

        private const int DefaultAttackSpeed = 2000;

        private readonly Fireball fireball;

        private Timer hitTimer;

        public Monk(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor, DefaultAttackSpeed)
        {
            this.fireball = Fireball.CreateFireball();
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
        }

        public Monk(string name)
            : this(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor)
        {
        }

        #region Default Values

        public static int DefaultMonkHealth
        {
            get
            {
                return DefaultHealth;
            }
        }

        public static int DefaultMonkArmor
        {
            get
            {
                return DefaultArmor;
            }
        }

        public static int DefaultMonkDamage
        {
            get
            {
                return DefaultDamage;
            }
        }

        public static int DefaultMonkMana
        {
            get
            {
                return DefaultMana;
            }
        }

        public static int DefaultMonkAttackSpeed
        {
            get
            {
                return DefaultAttackSpeed;
            }
        }

        #endregion Default Values

        public int FireballCastRange
        {
            get
            {
                return this.fireball.SpellRange;
            }
        }

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

        public bool FireballIsReady
        {
            get
            {
                return this.fireball.IsReady;
            }
        }

        public bool CastFireball(ICharacter enemy)
        {
            if (enemy is Npc && this.fireball.IsReady)
            {
                if (this.RemoveManaPointsAfterCast(this.fireball.ManaCost + (this.Level * 10)))
                {
                    enemy.ReceiveHit(this.fireball.Cast(this.Level), AttackType.Magical);
                    this.fireball.IsReady = false;
                    return true;
                }
            }

            return false;
        }

        void HitTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.IsReadyToAttack)
            {
                this.hitTimer.Stop();
            }

            this.IsReadyToAttack = true;
        }
    }
}

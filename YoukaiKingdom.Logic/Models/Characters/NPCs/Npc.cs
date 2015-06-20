namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using System;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    /// <summary>
    /// Base class for non-player characters
    /// </summary>
    public abstract class Npc : Character
    {
        protected Npc(int level, string name, int health, int mana, int damage, int armor)
            : base(level, name, health, mana, damage, armor)
        { }

        public virtual void RemoveHealthPoints(int damage)
        {
            this.Health -= damage;
        }

        public override void Hit(ICharacter target)
        {
            if (target is Hero)
            {
                var targetPlayer = (Hero)target;
                targetPlayer.ReceiveHit(this.Damage, AttackType.Magical);
            }
        }

        public override void ReceiveHit(int damage, AttackType type)
        {
            if (type == AttackType.Physical)
            {
                int dmg = Math.Max(0, damage - (this.Armor - (this.Level * 50)));
                this.RemoveHealthPoints(dmg);
            }
            else if (type == AttackType.Magical)
            {

                this.RemoveHealthPoints(damage);
            }
        }
    }
}


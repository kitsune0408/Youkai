namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Characters.Spells;

    public class NpcMage : Npc
    {
        private readonly Fireball fireball;

        public NpcMage(int level, string name, int health, int mana, int damage, int armor, int attackSpeed)
            : base(level, name, health, mana, damage, armor, attackSpeed)
        {
            this.fireball = Fireball.CreateFireball();
        }

        public override void Hit(ICharacter target)
        {
            if (target is Hero)
            {
                var targetPlayer = (Hero)target;
                targetPlayer.ReceiveHit(this.Damage, AttackType.Physical);
            }
        }

        public void CastFireball(ICharacter enemy)
        {
            if (enemy is Hero)
            {
                int npcDamage = this.fireball.Cast(this.Level) - 100;
                enemy.ReceiveHit(npcDamage, AttackType.Magical);
            }
        }
    }
}

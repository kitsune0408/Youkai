namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    public class NpcWarrior : Npc
    {
        public NpcWarrior(int level, string name, int health, int mana, int damage, int armor, int attackSpeed) : base(level, name, health, mana, damage, armor, attackSpeed) { }

        public override void Hit(Interfaces.ICharacter target)
        {
            if (target is Hero)
            {
                var targetPlayer = (Hero)target;
                targetPlayer.ReceiveHit(this.Damage, AttackType.Physical);
            }
        }
    }
}

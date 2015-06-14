namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using System;

    using YoukaiKingdom.Logic.Interfaces;

    public class NpcWarrior : Npc
    {
        public NpcWarrior(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public override void Hit(ICharacter target)
        {
            throw new NotImplementedException();
        }
    }
}

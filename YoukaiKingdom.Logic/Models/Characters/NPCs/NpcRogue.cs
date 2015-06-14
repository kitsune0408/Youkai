namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using YoukaiKingdom.Logic.Interfaces;

    public class NpcRogue : Npc
    {
        public NpcRogue(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public override void Hit(ICharacter target)
        {
            throw new System.NotImplementedException();
        }
    }
}

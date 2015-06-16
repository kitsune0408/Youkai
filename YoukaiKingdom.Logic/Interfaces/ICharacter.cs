namespace YoukaiKingdom.Logic.Interfaces
{
    public interface ICharacter
    {
        string Name { get; set; }

        int MaxHealth { get; set; }

        int Health { get; set; }

        int MaxMana { get; set; }

        int Mana { get; set; }

        int Damage { get; set; }

        int Armor { get; set; }

        void Hit(ICharacter target);

        void ReceiveHit(ICharacter enemy);
    }
}

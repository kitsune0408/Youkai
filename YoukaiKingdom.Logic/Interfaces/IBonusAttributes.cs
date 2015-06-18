namespace YoukaiKingdom.Logic.Interfaces
{
    public interface IBonusAttributes
    {
        bool HasBonuses { get; }

        int АdditionalHealth { get; }

        int АdditionalMana { get; }

        int АdditionalDamage { get; }

        int АdditionalArmor { get; }
    }
}
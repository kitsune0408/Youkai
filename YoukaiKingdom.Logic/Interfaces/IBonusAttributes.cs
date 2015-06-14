namespace YoukaiKingdom.Logic.Interfaces
{
    public interface IBonusAttributes
    {
        bool HasBonuses { get; set; }

        int АdditionalHealth { get; set; }

        int АdditionalMana { get; set; }

        int АdditionalDamage { get; set; }

        int АdditionalArmor { get; set; }

        void GenerateBonusAttributes();
    }
}
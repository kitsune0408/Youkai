namespace YoukaiKingdom.Logic.Interfaces
{
    using YoukaiKingdom.Logic.Models.Items.BonusAttributes;

    public interface IArmor
    {
        int DefensePoints { get; set; }

        BonusAttributes Bonus { get; set; }
    }
}

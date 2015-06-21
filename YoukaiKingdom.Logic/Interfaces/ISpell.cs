namespace YoukaiKingdom.Logic.Interfaces
{
    public interface ISpell
    {
        int Damage { get; }

        int ManaCost { get; }

        double CastInterval { get; }

        int SpellRange { get; set; }
    }
}
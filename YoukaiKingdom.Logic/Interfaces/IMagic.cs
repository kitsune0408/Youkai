namespace YoukaiKingdom.Logic.Models.Characters.Magics
{
    public interface IMagic
    {
        int Damage { get; }

        int ManaCost { get; }

        double CastInterval { get; }
    }
}
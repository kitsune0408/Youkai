namespace YoukaiKingdom.Logic
{
    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    public class GameEngine
    {
        public GameEngine(Hero heroClass)
        {
            this.HeroClass = heroClass;
        }

        public Hero HeroClass { get; set; }

        public void Start()
        {

        }
    }
}

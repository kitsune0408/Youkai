using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace YoukaiKingdom.GameScreens
{
    public abstract class BaseGameScreen: DrawableGameComponent
    {
        protected MainGame MGame;

        public BaseGameScreen(MainGame mGame):base(mGame)
        {
            this.MGame = mGame;
        }

        protected override void LoadContent()
        {
            ContentManager content = Game.Content;
            base.LoadContent();
        }

    }
}

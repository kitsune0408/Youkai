using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace YoukaiKingdom.GameScreens
{
    public abstract class BaseGameScreen: DrawableGameComponent
    {
        protected MainGame MGame;

        protected BaseGameScreen(MainGame mGame):base(mGame)
        {
            this.MGame = mGame;
        }
    }
}

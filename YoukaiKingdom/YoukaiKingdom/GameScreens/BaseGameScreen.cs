using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;



namespace YoukaiKingdom.GameScreens
{
    public abstract class BaseGameScreen: DrawableGameComponent
    {
        protected MainGame MGame;
        protected SoundEffect buttonSelectionSoundEffect;

        protected BaseGameScreen(MainGame mGame):base(mGame)
        {
            this.MGame = mGame;
            this.buttonSelectionSoundEffect = MGame.Content.Load<SoundEffect>("Sounds/Clack_SFX");
        }

        public bool Paused { get; set; }

        protected void PlaySound(object sender, EventArgs e)
        {
            buttonSelectionSoundEffect.Play(0.5f, 0.0f, 0.0f);
        }
    }
}
